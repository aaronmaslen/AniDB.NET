/* Copyright 2011 Aaron Maslen. All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 
 *  1. Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 * 
 *  2. Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in the
 *     documentation and/or other materials provided with the distribution.
 * 
 * THIS SOFTWARE IS PROVIDED ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
 * AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL
 * THE FOUNDATION OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 * INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace libAniDB.NET
{
	/// <summary>
	/// Implementation of the IAniDB interface
	/// </summary>
	public partial class AniDB
	{
		public const int ProtocolVersion = 3;

		public string SessionKey { get; private set; }

		public int Timeout { get; set; }

		private readonly ConcurrentDictionary<string, AniDBRequest> _sentRequests;
		private readonly ConcurrentBag<string> _tags;

		private readonly TokenBucket<AniDBRequest> _sendBucket;

		private const uint MinSendDelay = 2000;
		private const uint AvgSendDelay = 4000;
		private const int BurstLength = 60000;

		private readonly Encoding _encoding;

		public readonly string ClientName;
		public readonly int ClientVer;

		private readonly UdpClient _udpClient;

		public AniDB(int localPort, string clientName = "libanidbdotnet", int clientVer = 1, Encoding encoding = null,
		             string remoteHostName = "api.anidb.net", int remotePort = 9000)
		{
			Timeout = 20000;

			ClientName = clientName;
			ClientVer = clientVer;

			_encoding = encoding ?? Encoding.ASCII;

			_sentRequests = new ConcurrentDictionary<string, AniDBRequest>();

			_udpClient = new UdpClient(remoteHostName, remotePort);
			_udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, localPort));

			new Thread(RecievePackets).Start();

			_sendBucket = new TokenBucket<AniDBRequest>(MinSendDelay, AvgSendDelay,
			                                            BurstLength / (AvgSendDelay - MinSendDelay), true,
														SendPacket);
		}

		private void QueueCommand(AniDBRequest request)
		{
			if(_sentRequests.ContainsKey(request.Tag))
				throw new ArgumentException("A request with that tag has already been sent");

			if(SessionKey != "")
				((Dictionary<string,string>)request.ParValues).Add("s", SessionKey);

			_sendBucket.Input(request);
		}

		private void SendPacket(AniDBRequest request)
		{
			request.ToByteArray(_encoding);

			byte[] requestBytes = request.ToByteArray(_encoding);

			_udpClient.Send(requestBytes, requestBytes.Count());

			if (!_sentRequests.TryAdd(request.Tag, request))
				request.Callback(null, request);
		}

		private void RecievePackets()
		{
			while(_udpClient.Client.Connected)
			{
				IPEndPoint remoteEP = (IPEndPoint)_udpClient.Client.RemoteEndPoint;
				remoteEP = new IPEndPoint(remoteEP.Address, remoteEP.Port);

				byte[] responseBytes = _udpClient.Receive(ref remoteEP);
				AniDBResponse response = new AniDBResponse(responseBytes);
				new Thread(() => HandleResponse(response)).Start();
			}
		}

		private void HandleResponse(AniDBResponse response)
		{
			if (response.Code == AniDBResponse.ReturnCode.LOGIN_ACCEPTED ||
				response.Code == AniDBResponse.ReturnCode.LOGIN_ACCEPTED_NEW_VER)
					SessionKey = response.ReturnString.Split(new [] {' '}, 1)[0];

			if (response.Code == AniDBResponse.ReturnCode.LOGGED_OUT ||
				response.Code == AniDBResponse.ReturnCode.LOGIN_FAILED ||
				response.Code == AniDBResponse.ReturnCode.LOGIN_FIRST)
					SessionKey = "";

			AniDBRequest request;
			if(_sentRequests.TryRemove(response.Tag, out request))
				request.Callback(response, request);
		}

		~AniDB()
		{
			_udpClient.Close();
		}
	}
}
