using System;

namespace BinarySearchTree
{
    class Program
    {
        static void Main(string[] args)
        {
            var bst = new BinarySearchTree<int>();
            bst.Insert(42);
            bst.Insert(2);
            bst.Insert(4);
            bst.Insert(0);
            bst.Insert(98);
            bst.Insert(150);
            bst.Insert(120);
            bst.Insert(38);
            bst.Insert(35);
            bst.Insert(19);
            bst.Insert(8);

            foreach (var i in bst.TraverseInOrder())
            {
                Console.WriteLine(i);
            }

            Console.WriteLine($"MIN: {bst.Min()}");
            Console.WriteLine($"MAX: {bst.Max()}");
            bst.Get(19).IfSome(
                v => Console.WriteLine(v.Value) 
                );

            bst.Remove(98);
            foreach (var i in bst.TraverseInOrder())
            {
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }
    }
}
