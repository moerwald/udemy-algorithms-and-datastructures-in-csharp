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

        public void Remove(T value)
        {
            root = Remove(root, value);
        }

        private TreeNode<T> Remove(TreeNode<T> subtreeRoot, T value)
        {
            if (subtreeRoot == null)
            {
                return null;
            }

            var compareTo = value.CompareTo(subtreeRoot.Value);
            if (compareTo < 0)
            {
                // Value to remove is less than value of actual node -> move left through the tree
                subtreeRoot.LeftChild = Remove(subtreeRoot.LeftChild, value);
            }
            else if (compareTo > 0)
            {
                // Value to remove is greater than value of actual node -> move right through the tree
                subtreeRoot.RightChild = Remove(subtreeRoot.RightChild, value);
            }
            else
            {
                // We reached the value to remove
                if (subtreeRoot.LeftChildIsLeaf())
                {
                    return subtreeRoot.RightChild;
                }

                if (subtreeRoot.RightChildIsLeaf())
                {
                    return subtreeRoot.LeftChild;
                }

                // Actual node has two childs
                subtreeRoot.Value = subtreeRoot.RightChild.Min();

                // Move on through the right tree to remove the leaf node we've taken the value from
                subtreeRoot.RightChild = Remove(subtreeRoot.RightChild, subtreeRoot.Value);

            }
            return subtreeRoot;
        }
    }
}
