namespace Miniproject_1_Sorting_Shakespeare
{
    public static class MergeSort
    {
        
        public static void Run(string[] words)
        {
            string[] aux = new string[words.Length];

            var lo = 0;
            var hi = words.Length - 1;

            Split(words, aux, lo, hi);
        }

        private static void Split(string[] words, string[] aux, int lo, int hi)
        {
            if (hi <= lo) return;

            var mid = (hi - lo) / 2 + lo;

            Split(words, aux, lo, mid);
            Split(words, aux, mid+1, hi);
            Merge(words, aux, lo, mid, hi);
        }

        private static void Merge(string[] words, string[] aux, int lo, int mid, int hi)
        {
            int left = lo, right = mid+1;

            for (var i = lo; i <= hi; i++)
            {
                aux[i] = words[i];
            }
        
            for (var k = lo; k <= hi; k++)
            {
                if (left > mid) words[k] = aux[right++];
                else if (right > hi) words[k] = aux[left++];
                else if (UtilityMethods.Less(aux[right], aux[left])) words[k] = aux[right++];
                else words[k] = aux[left++];
            }
        }

    }
}