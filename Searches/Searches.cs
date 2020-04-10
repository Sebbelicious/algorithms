using System;
using System.IO;
using System.Linq;

namespace Searches
{
    public class Searches
    {
        public int ComparisonCount;
        public long[] Nums;

        public Searches(string path = @"C:\Users\s_ele\RiderProjects\Algoritmer og Datastrukturer\Searches\many-sorted-numbers.txt")
        {
            Nums = ReadNums(path);
        }

        public long[] ReadNums(string path = @"C:\Users\s_ele\RiderProjects\Algoritmer og Datastrukturer\Searches\many-sorted-numbers.txt")
        {
            try
            {
                string[] strings = File.ReadAllLines(path);
                long[] longs = strings.Select(long.Parse).ToArray();
                return longs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        public int Sequential(long num)
        {
            ComparisonCount = 0;
            for (int i = 0; i < Nums.Length; i++)
            {
                if (Compare(Nums[i], num) == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public int ExponentialSearch(long num)
        {
            ComparisonCount = 0;
            int comp = Compare(Nums[0], num);
            if (comp == 0)
            {
                return 0;
            }
            int lo = 1;
            int hi = 1;
            while(comp <= 0)
            {
                if (lo > Nums.Length)
                {
                    return -1;
                }
                lo = hi;
                hi = 1 << ComparisonCount;
                comp = Compare(Nums[hi], num);
            }

            return BinarySearch(lo, hi, num);

        }

        public int InterpolationSearch(long num)
        {
            ComparisonCount = 0;
            int len = Nums.Length;
            int lo = 0;
            int hi = len-1;

            while (lo < hi)
            {
                //Console.WriteLine("lo : " +lo);
                //Console.WriteLine("hi : " +hi);
                double probe = (double)(num - Nums[lo]) / (Nums[hi] - Nums[lo]);
                //Console.WriteLine("Probe : " + probe);
                int mid = (int) ((hi - lo)* probe+lo);
                int comp = Compare(Nums[mid], num);
                if (comp < 0)
                {
                    lo = mid+1;
                }
                else if (comp > 0)
                {
                    hi = mid;
                }
                else 
                {
                    return mid;
                }
                //Console.WriteLine("index : " +mid);
                //Console.WriteLine("value : " +Nums[mid]);
            }

            return -1;
        }

        public int BinarySearch(int lo, int hi, long num)
        {
            while (lo < hi)
            {
                var mid = (hi + lo)/2;
                int res = Compare(Nums[mid], num);
                if (res < 0)
                {
                    lo = mid+1;
                }
                else if (res > 0)
                {
                    hi = mid;
                }
                else
                {
                    return mid;
                }
                
            }

            return -1;
        }
        
        
        private int Compare ( long a, long b) {
            ComparisonCount++;
            return a > b ? 1 : a == b ? 0 : - 1;
        }
    }
}