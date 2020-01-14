using FluentOptionals;
using System;
using System.Collections.Generic;

namespace BinarySearchTree
{
    public class TreeNode<T> where T : IComparable<T>
    {
        private TreeNode<T> leftChild;
        private TreeNode<T> rightChild;

        public T Value { get;  set; }
        public TreeNode<T> LeftChild { get => leftChild;  set => leftChild = value; }
        public TreeNode<T> RightChild { get => rightChild;  set => rightChild = value; }

        public TreeNode(T value) => Value = value;

        public bool LeftChildIsLeaf() => LeftChild == null;
        public bool RightChildIsLeaf() => RightChild == null;

        public void Insert(T newValue)
        {
            new ValueComparer<T>(newValue).CompareTo(
                this.Value,
                () => InsertRecursiveOrSetLeaf(ref leftChild, newValue),
                () => InsertRecursiveOrSetLeaf(ref rightChild, newValue));

            // Locals
            static void InsertRecursiveOrSetLeaf(ref TreeNode<T> node, T value)
            {
                if (node != null)
                {
                    node.Insert(value);
                }
                else
                {
                    node = new TreeNode<T>(value);
                }

            }
        }

        public Optional<TreeNode<T>> Get(T value)
            => new ValueComparer<T>(value)
                    .CompareTo(
                        this.Value,
                        less: () => LeftChild?.Get(value) ?? Optional.None<TreeNode<T>>(),
                        greater: () => RightChild?.Get(value) ?? Optional.None<TreeNode<T>>(),
                        equal: () => Optional.From(this)
                    );

        public IEnumerable<T> TraverseInOrder() => InnerTraverse(new List<T>());

        public T Min()
        {
            if (LeftChild != null)
            {
                return LeftChild.Min();
            }

            return this.Value;
        }

        public T Max()
        {
            if (RightChild != null)
            {
                return RightChild.Max();
            }

            return this.Value;
        }

        private IEnumerable<T> InnerTraverse(List<T> list)
        {
            if (LeftChild != null)
            {
                LeftChild.InnerTraverse(list);
            }

            list.Add(this.Value);

            if (RightChild != null)
            {
                RightChild.InnerTraverse(list);
            }

            return list;
        }
    }
}
