using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace libAniDB.Net
{
	class ConcurrentList<T> : IEnumerable<T>
	{
		private readonly ConcurrentDictionary<int, T> _internalDictionary = new ConcurrentDictionary<int, T>();
		private readonly object _lock = new object();

		#region Implementation of IEnumerable

		/// <summary>
		/// Returns an enumerator that iterates through the collection.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<T> GetEnumerator()
		{
			return _internalDictionary.Values.GetEnumerator();
		}

		/// <summary>
		/// Returns an enumerator that iterates through a collection.
		/// </summary>
		/// <returns>
		/// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
		/// </returns>
		/// <filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		public bool TryAdd(T item)
		{
			if (_internalDictionary.IsEmpty)
				return _internalDictionary.TryAdd(0, item);

			lock (_lock)
				return _internalDictionary.TryAdd(_internalDictionary.Keys.Last() + 1, item);
		}

		public bool TryRemove(T item)
		{
			lock (_lock)
			{
				if (_internalDictionary.IsEmpty || !_internalDictionary.Any(v => v.Value.Equals(item)))
					return false;

				T removedItem;

				return _internalDictionary.TryRemove(_internalDictionary.First(i => i.Value.Equals(item)).Key,
				                                     out removedItem);
			}
		}

		public bool TryRemoveAt(int index, out T item)
		{
			return _internalDictionary.TryRemove(index, out item);
		}

		public int IndexOf(T item)
		{
			if (_internalDictionary.IsEmpty)
				return -1;

			lock(_lock)
				return _internalDictionary.First(i => i.Value.Equals(item)).Key;
		}

		public T this[int index]
		{
			get
			{
				T value;

				_internalDictionary.TryGetValue(index, out value);

				return value;
			}

			set
			{
				_internalDictionary.AddOrUpdate(index, value, (v, o) => value);
			}
		}
	}
}