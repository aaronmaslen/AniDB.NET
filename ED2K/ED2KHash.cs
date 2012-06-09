using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace ED2K
{
	public class ED2KHash
	{
		private class Chunk
		{
			private readonly byte[] _data;

			public Chunk(byte[] data)
			{
				if(data.Length > ChunkSize)
					throw new ArgumentException("Data is larger than chunk size");

				_data = data;
				Done = new EventWaitHandle(false, EventResetMode.ManualReset);
			}

			public MD4Digest MD4Digest { get; private set; }

			public EventWaitHandle Done { get; private set; }

			public bool Last;

			public void CalculateHash()
			{
				Done.Reset();
				MD4Digest = MD4Context.GetDigest(_data);
				Done.Set();
			}
		}

		private readonly  BlockingCollection<Chunk> _processQueue;
		private readonly FileStream _fileStream;

		public const int ChunkSize = 9728000;

		public ED2KHash(string filePath, int maxSimultaneousChunks)
		{
			_fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

			_processQueue = new BlockingCollection<Chunk>(new ConcurrentQueue<Chunk>(), maxSimultaneousChunks);
		}

		private string _hash;
		public string Hash
		{
			get
			{
				if (_hash == null)
					CalculateHash();

				return _hash;
			}
		}

		public void CalculateHash()
		{
			Complete = 0;

			MD4Context md4Context = new MD4Context();

			if (ChunkCount == 1)
			{
				byte[] data = new byte[_fileStream.Length];
				_fileStream.Read(data, 0, data.Length);

				Chunk newChunk = new Chunk(data);
						
				newChunk.CalculateHash();

				_hash = newChunk.MD4Digest.ToString();

				Complete = 1;
				return;
			}

			Parallel.Invoke(new Action[]
					            {
					                () =>
					                	{
											for (int i = 0; i < ChunkCount; i++)
											{
												byte[] data = new byte[_fileStream.Length - _fileStream.Position > ChunkSize
																		? ChunkSize
																		: _fileStream.Length - _fileStream.Position];

												_fileStream.Read(data, 0, data.Length);

												Chunk newChunk = new Chunk(data) {Last = _fileStream.Position == _fileStream.Length};
												ThreadPool.QueueUserWorkItem(o => newChunk.CalculateHash());

												//Blocks if the queue is full
												_processQueue.Add(newChunk);
											}
					                	},
									() =>
										{
											Chunk currentChunk;
											do
											{
												//Blocks if the queue is empty
												currentChunk = _processQueue.Take();

												//Blocks if the chunk hash is still calculating
												currentChunk.Done.WaitOne();

												byte[] chunkHash = currentChunk.MD4Digest.ToArray();

												md4Context.Update(chunkHash, 0, chunkHash.Length);

												Complete++;
											} while (!currentChunk.Last);
										}
					            });

			_hash = md4Context.GetDigest().ToString();
		}

		public int ChunkCount {
			get
			{
				return (int) _fileStream.Length / ChunkSize + 1;
			}
		}

		public int Complete { get; private set; }
	}
}
