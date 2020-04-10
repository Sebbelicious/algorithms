using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mini_Project_3_Searching_Shakespeare
{
    internal static class Program
    {
        public static void Main()
        {
            //For testing a smaller file
            // var text = TextProcessor.ReadText(@"..\..\test-text.txt"); //Change path to your own root folder, if this does not work

            // Reading the whole text from file to a string
            var text = TextProcessor.ReadText(); //For doing the sort on the complete text

            //Find amount of chars in the alphabet for the text
            //var alphabetCount = TextProcessor.FindAlphabetCount(text);

            // // Build suffixtree and save to a jsonfile:
            // //Build SuffixTree from shakespeare text
            //Start a stopwatch
            var watch1 = Stopwatch.StartNew();
            var suffixTree = new SuffixTree(text);
            //Stop stopwatch
            watch1.Stop();
            Console.WriteLine($"Total time for building suffixtree: {watch1.ElapsedMilliseconds} milliseconds");

            // // serialize JSON directly to a file
            // using (var file = File.CreateText(@"C:\Users\s_ele\RiderProjects\Algoritmer og Datastrukturer\Mini Project 3 Searching Shakespeare\suffixtree.json"))
            // {
            //     var serializer = new JsonSerializer();
            //     serializer.Serialize(file, suffixTree);
            // }
            //
            // //Read suffixtree from a jsonfile
            // var watch2 = Stopwatch.StartNew();
            // SuffixTree suffixTree2;
            // using (var file = File.OpenText(@"C:\Users\s_ele\RiderProjects\Algoritmer og Datastrukturer\Mini Project 3 Searching Shakespeare\suffixtree.json"))
            // {
            //     var serializer = new JsonSerializer();
            //     suffixTree2 = (SuffixTree)serializer.Deserialize(file, typeof(SuffixTree));
            // }
            // watch2.Stop();
            // Console.WriteLine($"Total time for reading suffixtree from a jsonfile: {watch2.ElapsedMilliseconds} milliseconds"); 

            //Search
            const string searchStr = "to be, or";
            var watch3 = Stopwatch.StartNew();
            var res = (List<string>) suffixTree.Search(searchStr, 60);
            watch3.Stop();
            Console.WriteLine($"Total time for searching for {searchStr}: {watch3.ElapsedMilliseconds} milliseconds");

            Console.WriteLine("Search results: ");
            foreach (var str in res)
            {
                Console.WriteLine(str);
                Console.WriteLine("------------------------------------------------------------------------");
            }

            //Testing stuff
            // var resTest = res.FindAll(str => str.Contains("to be, or"));
            // foreach (var str in resTest)
            // {
            //     Console.WriteLine(str);
            //     Console.WriteLine("------------------------------------------------------------------------");
            // }
        }
    }
}