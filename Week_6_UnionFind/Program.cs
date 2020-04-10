using System;
using Week_6_UnionFind;

namespace ConsoleApplication1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var amount = 10;
            var qf = new WeightedQuickUnion(amount);
            for (var i = 0; i < amount - 1; i++)
            {
                qf.Union(i, i + 1);
                Console.WriteLine(qf.Count());
            }
        }
    }
}