using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Xml;

namespace Miniproject_1_Sorting_Shakespeare
{
    public static class TextProcessor
    {

        //Reads the textfile (with a default path) and returns the text as a string
        public static string ReadText(string path = @"C:\Users\s_ele\RiderProjects\Algoritmer og Datastrukturer\Miniproject 1 Sorting Shakespeare\shakespeare-complete-works.txt")
        {
            try
            {
                return System.IO.File.ReadAllText(path);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        //Finds all the words with regex and puts them into a string array
        public static string[] SanitizeText(string text)
        {
            try
            {
                
                text = text.ToLower();
            
                string pattern = @"[a-z][a-z'-]*[a-z]?|[a-z]";

                var words = Regex.Matches(text, pattern)
                    .Cast<Match>()
                    .Select(m => m.Value)
                    .ToArray();
            
                return words;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        //Takes a string array and puts them back into a joined string
        public static string BackToString(string[] words)
        {
            return string.Join(" ", words);
        }
    }
}