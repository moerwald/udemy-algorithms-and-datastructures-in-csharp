using FluentOptionals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BinarySearchTree
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public Optional<TreeNode<T>> Get(T value)
            => root?.Get(value) ?? Optional.None<TreeNode<T>>();

        public T Min()
        {
            if (root == null)
            {
                throw new InvalidOperationException("root is not set");
            }
            return root.Min();
        }

        public T Max()
        {
            if (root == null)
            {
                throw new InvalidOperationException("root is not set");
            }
            return root.Max();
        }

        public void Insert (T value)
        {
            if (root == null)
            {
                root = new TreeNode<T>(value);
                return;
            }

            root.Insert(value);
        }

        public IEnumerable<T> TraverseInOrder()
        {
            if (root == null)
            {
                return Enumerable.Empty<T>();

            }

            return root.TraverseInOrder();

        }
    }
}
