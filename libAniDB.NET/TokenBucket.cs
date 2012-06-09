using System.Collections.Concurrent;
using System.Threading;

namespace libAniDB.NET
{
	internal class TokenBucket<T>
	{
		private uint _outDelay;

		public uint OutDelay
		{
			get { return _outDelay; }
			set
			{
				_outDelay = value;
				_outTimer.Change(value, value);
			}
		}

		private uint _tokenAddDelay;

		public uint TokenAddDelay
		{
			get { return _tokenAddDelay; }
			set
			{
				_tokenAddDelay = value;
				_tokenAddTimer.Change(value, value);
			}
		}

		public uint TokenCapacity { get; set; }

		private readonly Timer _outTimer;
		private readonly Timer _tokenAddTimer;

		private readonly ConcurrentQueue<T> _outputQueue;

		private uint _tokens;

		public delegate void TokenBucketCallBack(T output);

		public TokenBucket(uint outDelay, uint tokenAddDelay, uint tokenCapacity, bool startFilled,
		                   TokenBucketCallBack outputCallBack)
		{
			_outputQueue = new ConcurrentQueue<T>();

			TokenCapacity = tokenCapacity;

			_tokens = startFilled ? TokenCapacity : 0;

			_outDelay = outDelay;
			_tokenAddDelay = tokenAddDelay;

			//I could probably do this with one timer, but I cbf figuring it out
			_outTimer = new Timer(Output, outputCallBack, outDelay, outDelay);
			_tokenAddTimer = new Timer(TokenAdd, null, tokenAddDelay, tokenAddDelay);
		}

		private void Output(object state)
		{
			T outputObject;

			do
			{
				if (_outputQueue.IsEmpty)
					return;
			} while (!_outputQueue.TryDequeue(out outputObject)); //This is probably VERY bad... ohwell, I'll fix later

			_tokens--;

			((TokenBucketCallBack)state)(outputObject);
		}

		private void TokenAdd(object state)
		{
			if (_tokens < TokenCapacity)
				_tokens++;
		}

		public void Input(T input)
		{
			_outputQueue.Enqueue(input);
		}
	}
}
