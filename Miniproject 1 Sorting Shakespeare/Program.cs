using System;
using System.Diagnostics;
using System.Reflection;

namespace Miniproject_1_Sorting_Shakespeare
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            //For testing a smaller file
            // var text = TextProcessor.ReadText(@"C:\Users\s_ele\RiderProjects\Algoritmer og Datastrukturer\Miniproject 1 Sorting Shakespeare\test-text.txt");
            
            // Reading the whole text from file to a string
            var text = TextProcessor.ReadText(); //For doing the sort on the complete text
            
            //Taking all words from the string and putting them in a string-array
            var words = TextProcessor.SanitizeText(text);
            
            //Running the selected sort-method on the string-array
            // Change the second parameter below to change what sort-method-Class to use from these choices: typeof(SelectionSort), typeof(InsertionSort), typeof(MergeSort), typeof(HeapSort), typeof(TrieSort);
            SortAndTime(words, typeof(MergeSort));

            //Putting the sorted words back into a string
            var stringSorted = TextProcessor.BackToString(words);
            
            //Use the below line to print out the finished sorted string
            // Console.WriteLine(stringSorted);
            
            
            //Method for running sorting-method by algoName and printing out time and words/millisecond
            static int SortAndTime(string[] words, IReflect sortClassName)
            {
                //Finds the matching method name "Run" from the Class given as parameter sortClassName
                var sortMethod = sortClassName?.GetMethod("Run", BindingFlags.Static | BindingFlags.Public);
                
                //Start a stopwatch
                var watch = Stopwatch.StartNew();
                //Invoke the found sortingmethod (null because the class is static) with the string array as parameter (words)
                sortMethod?.Invoke(null, new object[] {words});
                
                //Stop stopwatch
                watch.Stop();
                

                Console.WriteLine($"Total time for {sortClassName} sorting the words is: {watch.ElapsedMilliseconds} milliseconds"); 
                
                var wordsPerMilisecond = (int) (words.Length / watch.ElapsedMilliseconds);
                Console.WriteLine($"The number of words per millisecond for {sortClassName} sorting the words is: {wordsPerMilisecond}");

                return wordsPerMilisecond;
            }
            //
            // //Another way of running the chosen method
            // static int WordsPerMillisecond2(string[] words, string algoName)
            // {
            //     var watch = Stopwatch.StartNew();
            //
            //     switch (algoName.ToLower())
            //     {
            //         case "selection":
            //             SelectionSort.Run(words);
            //             break;
            //     
            //         case "insertion":
            //             InsertionSort.Run(words);
            //             break;
            //     
            //         case "merge":
            //             MergeSort.Run(words); //Top Down Merge sort
            //             break;
            //     
            //         case "heap":
            //             HeapSort.Run(words);
            //             break;
            //     
            //         case "trie":
            //             TrieSort.Run(words);
            //             break;
            //     }
            //     watch.Stop();
            //     
            //
            //     Console.WriteLine($"Total time for {algoName} sorting the words is: {watch.ElapsedMilliseconds} milliseconds"); 
            //     
            //     var wordsPerMilisecond = (int) (words.Length / watch.ElapsedMilliseconds);
            //     Console.WriteLine($"The number of words per millisecond for {algoName} sorting the words is: {wordsPerMilisecond}");
            //
            //     return wordsPerMilisecond;
            // }
            
        }
    }
}
