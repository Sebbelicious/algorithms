namespace Miniproject_1_Sorting_Shakespeare
{
    public static class SelectionSort
    {
        
        public static void Run(string[] words)
        {
            for (var i = 0; i < words.Length; i++)
            {
                var min = i;
                for (var j = i + 1; j < words.Length; j++)
                {
                    if (UtilityMethods.Less(words[j], words[min]))
                    {
                        min = j;
                    }
                }

                UtilityMethods.Swap(words, i, min);
            }
        }
    }
}