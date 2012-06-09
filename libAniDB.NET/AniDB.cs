using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;

namespace libAniDB.NET
{
	/// <summary>
	/// Implementation of the IAniDB interface
	/// </summary>
	public partial class AniDB
	{
		/// <summary>
		/// Invoked after a packet is recieved and processed internally.
		/// If you want to do something with the packet before the library handles it, create a tagged request
		/// </summary>
		public event AniDBUnTaggedResponseCallback ResponseRecieved;

		public const int ProtocolVersion = 3;

		//public bool EncryptionEnabled { get; private set; }
		//private byte[] _encryptionKey;
		public string ApiPass;

		public bool LoggedIn { get; private set; }
		public string SessionKey { get; private set; }

		public int Timeout;

		//private readonly ConcurrentList<AniDBRequest> _sentRequests;
		//private readonly ConcurrentList<string> _tags;

		private readonly ConcurrentStack<AniDBRequest> _sentRequests;
		private readonly ConcurrentBag<string> _tags;

		private readonly TokenBucket<AniDBRequest> _sendBucket;

		private const uint MinSendDelay = 2000;
		private const uint AvgSendDelay = 4000;
		private const int BurstLength = 60000;

		private readonly Encoding _encoding;

		public readonly string ClientName;
		public readonly int ClientVer;

		public AniDB(int localPort, string clientName = "libanidbdotnet", int clientVer = 1, Encoding encoding = null,
		             string remoteHostName = "api.anidb.net", int remotePort = 9000, int timeout = 20000)
		{
			ClientName = clientName;
			ClientVer = clientVer;

			_encoding = encoding ?? Encoding.ASCII;

			_sendBucket = new TokenBucket<AniDBRequest>(MinSendDelay, AvgSendDelay,
			                                            BurstLength / (AvgSendDelay - MinSendDelay), true, null);
		}

		private void SendCommand(AniDBRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
