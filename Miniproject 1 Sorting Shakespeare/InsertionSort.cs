namespace Miniproject_1_Sorting_Shakespeare
{
    public static class InsertionSort
    {
        
        public static void Run(string[] words)
        {
            if (words.Length <= 1) return; //Can't compare if array has less than 2 values
            //Loop through the whole array
            for (var i = 1; i < words.Length; i++)
            {
                //Loop through the array left of i
                for (var j = i; j > 0 && UtilityMethods.Less(words[j], words[j-1]); j--)
                {
                    UtilityMethods.Swap(words, j, j-1);
                }
            }
        }

    }
}