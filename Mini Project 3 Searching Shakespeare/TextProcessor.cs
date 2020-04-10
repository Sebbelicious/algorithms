using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Mini_Project_3_Searching_Shakespeare
{
    public static class TextProcessor
    {
        //Reads the textfile (with a default path) and returns the text as a string with whitespaces replaced with spaces
        public static string
            //Change the path to your own location of the file if this does not work
            ReadText(string path = @"..\..\shakespeare-complete-works.txt") 
        {
            try
            {
                var text = File.ReadAllText(path);

                //Replace all whitespaces with a space. (Build-optimization and cleaner search-results)
                return Regex.Replace(text, @"\s+", " ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        //Utility method to see the alphabet used in the text
        public static int FindAlphabetCount(string text)
        {
            text = text.ToLower();
            //Find number of characters in the text
            var alphabet = new Dictionary<char, char>();
            foreach (var ch in text)
            {
                if (!alphabet.ContainsKey(ch))
                    alphabet.Add(ch, ch);
            }

            return alphabet.Count;
        }
    }
}