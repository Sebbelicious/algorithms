using System;

namespace Assignment3_Balanced_Search_Tree
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            var keys = new [] {1, 45, 25, 67, 347, 23, 45, 89, 123, 10, 17, 112, 15, 14, 18, 20};
            var values = new [] {1, 45, 25, 67, 347, 23, 45, 89, 123, 10, 15, 15, 15, 15, 15, 15, 15, 15,1, 5, 15, 15, 15, 15, 15, 15};
            
            var rb = new RedBlackBST<int, int>();

            for (var i = 0; i < keys.Length; i++)
            {
                rb.Put(key: keys[i], values[i]);
            }

            Console.WriteLine(rb.IsBalanced());

            /*
            var keysList = rb.PrintTreeToArray();

            foreach (var key in keysList)
            {
                Console.WriteLine(key);
            }
            */

            /*
            var tree = rb.PrintTree();

            foreach (var key in tree)
            {
                Console.WriteLine(key);
            }
            */
        }
    }
}