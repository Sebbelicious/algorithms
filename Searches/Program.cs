using System;

namespace Searches
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var searches = new Searches();

            // searches.ComparisonCount = 0;
            // int i = searches.BinarySearch(0, searches.Nums.Length, 50113299);
            
            //int i = searches.ExponentialSearch(50113299);
            //int i = searches.InterpolationSearch(50113299); // 3 iterationer
            //int i = searches.InterpolationSearch(463516772); // Eksisterer ikke
            //int i = searches.InterpolationSearch(63109152);
            //int i = searches.InterpolationSearch(347556710);
            //int i = searches.InterpolationSearch(26206720);
            // int i = searches.InterpolationSearch(420);


            foreach (var num in searches.Nums)
            {
                int i = searches.InterpolationSearch(num);
                Console.WriteLine($"The index of {num} : {i}");
                Console.WriteLine("The comparisonCount is : " + searches.ComparisonCount);
                if (i == -1)
                {
                    break;
                }
            }
            int j = searches.InterpolationSearch(1912834);
            Console.WriteLine($"The index of 1912834 : {j}");
            Console.WriteLine("The comparisonCount is : " + searches.ComparisonCount);
        }
    }
}