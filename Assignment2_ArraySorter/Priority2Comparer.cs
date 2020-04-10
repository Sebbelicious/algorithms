using System.Collections.Generic;

namespace Assignment2_ArraySorter
{
    public class Priority2Comparer : IComparer<Data>
    {
        public int Compare(Data x, Data y)
        {
            if (x.Priority2 - y.Priority2 < 0)
            {
                return -1;
            }

            return x.Priority2 - y.Priority2 > 0 ? 1 : 0;
        }
    }
}