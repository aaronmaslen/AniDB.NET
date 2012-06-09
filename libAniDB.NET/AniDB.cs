using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using libAniDB.Net;
#if DEBUG
#endif

namespace libAniDB.NET {

	internal partial class AniDB
	{
		/// <summary>
		/// Invoked after a packet is recieved and processed internally.
		/// If you want to do something with the packet before the library handles it, create a tagged request
		/// </summary>
		public event AniDBUnTaggedResponseCallback ResponseRecieved;

		public const int ProtocolVersion = 3;

		public bool EncryptionEnabled { get; private set; }
		private byte[] _encryptionKey;
		public string ApiPass;

		public bool LoggedIn { get; private set; }
		public string SessionKey { get; private set; }

		public int Timeout;

		private readonly UdpClient _client;
		private bool _listening;

		private readonly ConcurrentList<AniDBRequest> _sentRequests;
		private readonly ConcurrentList<string> _tags;

		private readonly TokenBucket<AniDBRequest> _sendBucket;

		private const uint MinSendDelay = 2000;
		private const uint AvgSendDelay = 4000;
		private const int BurstLength = 60000;

		private readonly Encoding _encoding;

		public string ClientName { get; private set; }
		public int ClientVer { get; private set; }

		public AniDB(int localPort, string clientName = "libanidbdotnet", int clientVer = 1, Encoding encoding = null, string remoteHostName = "api.anidb.net", int remotePort = 9000, int timeout = 20000)
		{
			ClientName = clientName;
			ClientVer = clientVer;

			_sentRequests = new ConcurrentList<AniDBRequest>();
			_tags = new ConcurrentList<string>();
			_sendBucket = new TokenBucket<AniDBRequest>(MinSendDelay, AvgSendDelay,
												  BurstLength / (AvgSendDelay - MinSendDelay), true, SendItem);
			_encoding = encoding;

			Timeout = timeout;

			IPAddress[] addresses = Dns.GetHostAddresses(remoteHostName);

			foreach(IPAddress i in addresses) {
				try
				{
					_client = new UdpClient(new IPEndPoint(i.AddressFamily ==
					                                       AddressFamily.InterNetworkV6
					                                       	? IPAddress.IPv6Any
					                                       	: IPAddress.Any, localPort));

					_client.Connect(i, remotePort);

					break;
				}
				catch (SocketException e)
				{
					#if DEBUG
					Debug.Print(e.ToString());
					#endif
					continue;
				}
			}
		}

		public void Stop()
		{
			if(LoggedIn)
			{
				Logout((res, req) => _listening = false, "Logout");
				return;
			}

			_listening = false;
		}

		private void SendCommand(AniDBRequest request)
		{
			if(LoggedIn)
				request.ParValues.Add("s", SessionKey);

			if(request.Tag != "")
			{
				//Rather silly code to make sure that the same tag doesn't occur twice... I hope
				for(int i = 0; _tags.Contains(request.Tag); i++)
				{
					if (i > 0)
						request.Tag.Remove(request.Tag.Length - (i - 1).ToString().Length);

					request.Tag += i;
				}

				_tags.TryAdd(request.Tag);
			}

			_sendBucket.Input(request);
		}

		private void SendItem(AniDBRequest request)
		{
			byte[] dgram = request.ToByteArray(_encoding ?? Encoding.ASCII);

			_client.Send(dgram, dgram.Length);

			request.Timeout = Timeout;
			
			_sentRequests.TryAdd(request);

			if (_listening) return;

			_listening = true;
			_client.BeginReceive(RecievePacket, null);


			//TODO: Move this to a separate method? Lambdas like this look messy and are probably harder to maintain.
			(new Thread(() =>
			           	{
							while (_listening)
							{
								foreach (var v in _sentRequests)
									if (v.Timeout <= 0)
									{
										_sentRequests.TryRemove(v);

										if (v.Tag != "")
											v.Callback(null, v);

										//TODO: Invoke event with removed request so the client knows.
									}
									else v.Timeout -= 100;

								//TODO: Is there a better way to do this? Or a specific delay value considered "correct"?
								Thread.Sleep(100);
							}
			           	})).Start();
		} 

		private void RecievePacket(IAsyncResult state)
		{
			IPEndPoint remoteEndPoint = (IPEndPoint)_client.Client.RemoteEndPoint;

			byte[] response = _client.EndReceive(state, ref remoteEndPoint);

			(new Thread(ResponseHandler)).Start(response);

			if(_listening)
				_client.BeginReceive(RecievePacket, null);
		}

		private void ResponseHandler(object state)
		{
			byte[] responseBytes;

			if (((byte[]) state)[0] == 0 && ((byte[]) state)[1] == 0)
			{
				//Packet is compressed
				List<byte> deflatedBytes = new List<byte>();

				DeflateStream dfStream = new DeflateStream(new MemoryStream((byte[])state, false), CompressionMode.Decompress);

				int next;
				while((next = dfStream.ReadByte()) != -1)
				{
					deflatedBytes.Add((byte) next);
				}

				responseBytes = deflatedBytes.ToArray();
			}
			else
				responseBytes = (byte[]) state;

			AniDBResponse response = new AniDBResponse(responseBytes, _encoding);

			AniDBRequest request;
			if (response.Tag != "")
			{
				_tags.TryRemove(response.Tag);

				if (_sentRequests.TryRemoveAt(_sentRequests.IndexOf(_sentRequests.Where(
					i => i.Tag == response.Tag).First()), out request))
					request.Callback(response, request);
			}

			switch (response.ReturnCode)
			{
				case AniDBReturnCode.LOGIN_ACCEPTED:
				case AniDBReturnCode.LOGIN_ACCEPTED_NEW_VER:
					LoggedIn = true;
					SessionKey = response.ReturnString.Split(' ')[0];
					break;
				
				case AniDBReturnCode.LOGIN_FIRST:
					LoggedIn = false;
					SessionKey = "";
					break;

				case AniDBReturnCode.LOGGED_OUT:
					LoggedIn = false;
					break;

				case AniDBReturnCode.ENCRYPTION_ENABLED:
					//TODO: Finish encryption code
					MD5 md5Digest = MD5.Create();

					string key = ApiPass + response.ReturnString.Split(' ')[0];
					_encryptionKey = md5Digest.ComputeHash(_encoding != null ? _encoding.GetBytes(key) : Encoding.ASCII.GetBytes(key));

					EncryptionEnabled = true;
					break;
			}

			if(ResponseRecieved != null)
				ResponseRecieved(response);
		}

		~AniDB()
		{
			Stop();
		}
	}
}
