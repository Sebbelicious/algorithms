namespace Miniproject_1_Sorting_Shakespeare
{
    public static class UtilityMethods
    {
        //Method for swapping two values by index in an array
        public static void Swap(string[] words, int idxA, int idxB)
        {
            var tmp = words[idxA];
            words[idxA] = words[idxB];
            words[idxB] = tmp;
        }

        //Method for checking if the value of the first string is smaller than the second
        public static bool Less(string first, string second)
        {
            return string.CompareOrdinal(first, second) < 0;
        }
    }
}