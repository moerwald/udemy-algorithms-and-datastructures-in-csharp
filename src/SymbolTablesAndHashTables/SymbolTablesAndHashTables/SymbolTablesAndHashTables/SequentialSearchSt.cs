using System;
using System.Collections.Generic;

namespace SymbolTablesAndHashTables
{
    /// <summary>
    /// St = Single tabl
    /// Seraching/Inserting: O=(n)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SequentialSearchSt<TKey, TValue>
    {
        private class Node
        {
            public TKey Key { get; }
            public TValue Value { get; set; }

            public Node Next { get; set; }

            public Node(TKey key, TValue value, Node next)
            {
                Key = key;
                Value = value;
                Next = next;
            }
        }

        private Node _first;

        private readonly EqualityComparer<TKey> _comparer;

        public int Count { get; private set; }

        public SequentialSearchSt()
        {
            _comparer = EqualityComparer<TKey>.Default;
        }

        public SequentialSearchSt(EqualityComparer<TKey> comparer)
        {
            _comparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
        }

        public bool TryGet(TKey key, out TValue value)
        {
            value = default;
            for (var x = _first; x != null; x = x.Next)
            {
                if (KeysAreEqual(key, x.Key))
                {
                    value = x.Value;
                    return true;
                }
            }
            return false;
        }

        public void Add(TKey key, TValue value)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            for (var x = _first; x is object; x = x.Next)
            {
                if (KeysAreEqual(key, x.Key))
                {
                    x.Value = value;
                    return;
                }
            }

            // Attach new node at the begin of the list
            _first = new Node(key, value, _first);

            Count++;
        }

        public bool Contains(TKey key)
        {
            for (var x = _first; x is object; x = x.Next)
            {
                if (KeysAreEqual(key, x.Key))
                {
                    return true;
                }
            }

            return false;
        }

        public IEnumerable<TKey> Keys()
        {
            for (var x = _first; x is object; x = x.Next)
            {
                yield return x.Key;
            }
        }

        public bool Remove(TKey key)
        {
            if (key is null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (ListOnlyContainsOneElement() && KeysAreEqual(_first.Key, key))
            {
                Count--;
                _first = null;
                return true;
            }

            Node previousNode = null;
            for (var x = _first; x is object; x = x.Next)
            {
                if (KeysAreEqual(key, x.Key))
                {
                    // Remove the node
                    if (NodeIsFirstOne(x))
                    {
                        // Node is first one
                        _first = x.Next;
                    }
                    else
                    {
                        // Node has a previous and a next node
                        previousNode.Next = x.Next;
                    }

                    Count--;
                    return true;
                }

                previousNode = x;
            }

            return false;

            bool ListOnlyContainsOneElement() => Count == 1;
            bool NodeIsFirstOne(Node n) => n == _first;
        }

        private bool KeysAreEqual(TKey k1, TKey k2) => _comparer.Equals(k1, k2);
    }
}
