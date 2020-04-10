namespace Miniproject_1_Sorting_Shakespeare
{
    public static class HeapSort
    {
        public static void Run(string[] words)
        {
            var length = words.Length;
            //make heap
            for (var i = words.Length/2-1; i >= 0; i--)
            {
                Heapify(words, length, i);
            }
            //sort
            for (var i = words.Length-1; i >= 0; i--)
            {
                UtilityMethods.Swap(words, i, 0);
                Heapify(words, i, 0);
            }
        }

        private static void Heapify(string[] words, int length, int i)
        {
            var largest = i;

            var left = i * 2 + 1;
            var right = i * 2 + 2;

            if (left < length && UtilityMethods.Less(words[largest], words[left]))
            {
                largest = left;
            }
            if (right < length && UtilityMethods.Less(words[largest], words[right]))
            {
                largest = right;
            }

            if (i != largest)
            {
                UtilityMethods.Swap(words, i, largest);
                Heapify(words, length, largest);
            }
        }
    }
}