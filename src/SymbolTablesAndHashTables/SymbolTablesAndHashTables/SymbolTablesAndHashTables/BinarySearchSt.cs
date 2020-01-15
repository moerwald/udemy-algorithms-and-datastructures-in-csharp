using System;
using System.Collections.Generic;
using System.Text;

namespace SymbolTablesAndHashTables
{
    /// <summary>
    /// Searching O(log2 n), with every search step we decrease
    /// work at factor two (see the Rank method)
    /// Insertion O(2N)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class BinarySearchSt<TKey, TValue>
    {
        private TKey[] _keys;
        private TValue[] _values;

        public int Count { get; private set; }

        private readonly IComparer<TKey> _comparer;

        public int Capacity => _keys.Length;

        public bool IsEmpty => Count == 0;

        private const int DefaultCapacity = 4;

        public BinarySearchSt(int capacity, IComparer<TKey> comparer)
        {
            _keys = new TKey[capacity];
            _values = new TValue[capacity];
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));

        }

        public BinarySearchSt(int capacity) : this(capacity, Comparer<TKey>.Default)
        {
        }

        public BinarySearchSt() : this(DefaultCapacity, Comparer<TKey>.Default)
        {
        }

        /// <summary>
        /// Returns number of keys LESS than the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Rank(TKey key)
        {
            int low = 0;
            int high = Count - 1;
            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                int cmp = _comparer.Compare(key, _keys[mid]);
                if (cmp < 0)
                    high = mid - 1;
                else if (cmp > 0)
                    low = mid + 1;
                else
                    return mid;
            }

            return low;
        }

        public TValue GetValueOrDefault(TKey key)
        {
            if (IsEmpty)
            {
                return default;
            }

            int rank = Rank(key);
            if (rank < Count && _comparer.Compare(key, _keys[rank]) == 0)
            {
                return _values[rank];
            }

            return default;
        }

        public void Add(TKey key, TValue value)
        {
            if (key is null) { throw new ArgumentNullException(nameof(key)); }

            int rank = Rank(key);
            if (rank < Count && _comparer.Compare(key, _keys[rank]) == 0)
            {
                // Entry exists
                _values[rank] = value;
                return;
            }

            if (Count == Capacity)
            {
                Resize(Capacity * 2);
            }

            for (int i = Count; i > rank; i--)
            {
                // Shift to the right, until we've space in the rank
                // we want to add the new value
                _keys[i]   = _keys[i - 1];
                _values[i] = _values[i - 1];
            }

            _keys[rank] = key;
            _values[rank] = value;

            Count++;
        }

        public void Remove(TKey key)
        {
            if (key is null) { throw new ArgumentNullException(nameof(key)); }

            if (IsEmpty) { return; }

            int rank = Rank(key);
            if (rank==Count || _comparer.Compare(key, _keys[rank]) != 0)
            {
                // Key was not found
                return;
            }

            for (int i = rank; i < Count - 1; i++)
            {
                // Shif all keys to the left
                _keys[i] = _keys[i + 1];
                _values[i] = _values[i + 1];
            }

            Count--;

            _keys[Count] = default;
            _values[Count] = default;

            // resize if 1/4 full
            // if (Count > 0 && Count == keys.length/4) Resize (_key.lenght/2);

        }
        
        public bool Contains(TKey key)
        {
            int rank = Rank(key);
            if (rank < Count && _comparer.Compare(_keys[rank], key) == 0)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<TKey> Keys()
        {
            foreach (var item in _keys)
            {
                yield return item;
            }
        }

        public TKey Min()
        {
            if (IsEmpty) throw new InvalidOperationException("Table is empty");
            return _keys[0];
        }

        public TKey Max()
        {
            if (IsEmpty) throw new InvalidOperationException("Table is empty");
            return _keys[Count - 1];
        }

        public void RemoveMin() => Remove(Min());

        public void RemoveMax() => Remove(Max());

        public TKey Select (int index)
        {
            if (index < 0 || index >= Count)
                throw new ArgumentException("Index must be in valid range");

            return _keys[index];
        }

        public TKey Ceiling(TKey key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            var rank = Rank(key);
            if (rank == Count) return default;

            return _keys[rank];

        }
        public TKey Floor(TKey key)
        {
            if (key is null)
                throw new ArgumentNullException(nameof(key));

            var rank = Rank(key);
            if (rank == Count)
            {
                return _keys[Count - 1];
            }

            if (rank < Count && _comparer.Compare(_keys[rank], key)==0)
            {
                return _keys[rank];
            }

            return default;
        }

        public IEnumerable<TKey> Range(TKey left, TKey right)
        {
            var leftRank = Rank(left);
            var rightRank = Rank(right);
            if (rightRank == Count)
            {
                rightRank--;
            }

            for (int i = leftRank; i <= rightRank; i++)
            {
                yield return _keys[i];
            }
        }

        private void Resize(int capacity)
        {
            var keysTmp = new TKey[capacity];
            var valueTmp = new TValue[capacity];

            for (int i = 0; i < Count; i++)
            {
                keysTmp[i] = _keys[i];
                valueTmp[i] = _values[i];
            }

            _keys = keysTmp;
            _values = valueTmp;
        }
    }
}
