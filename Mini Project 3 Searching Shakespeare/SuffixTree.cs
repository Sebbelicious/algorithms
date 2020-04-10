using System;
using System.Collections.Generic;
using System.Linq;

namespace Mini_Project_3_Searching_Shakespeare
{
    public class SuffixTree
    {
        //Original Text is used when getting the searchresults
        public readonly string Text;

        //TextLower is used when building suffixtree to make alphabet less chars
        //and to ignore case when searching.  
        public readonly string TextLower;

        //_root node contains the whole suffixtree
        private readonly Node _root;

        public SuffixTree(string text)
        {
            Text = text;
            // -1 is given to signal non-values in root node
            _root = new LinkedNode(new Key(-1, -1), -1);
            TextLower = text.ToLower();

            //SuffixTree is made:
            var textLength = Text.Length;
            for (var i = 0; i < textLength; i++)
            {
                var key = new Key(i, textLength);
                _root.Add(TextLower, key, i);
            }
        }

        //Searches for a given searchstring and returns a list of strings of matches
        public IEnumerable<string> Search(string search, int maxResultAmount = 50)
        {
            var res = new List<string>(); //Searchresults in strings
            var resValues = new List<int>(); //Searchresults in startindex
            search = search.ToLower();
            const int extraCharAmount = 130; //Extra chars to add to each searchresult

            // Find the root node of the result
            //The parent node which holds all search-results in it's children
            var resNode = _root.Locate(TextLower, search);

            //No search matches
            if (resNode == null) return res;

            //Find all values of the searchmatch
            FindValuesOfChildren(resValues, resNode, maxResultAmount);
            resValues.Sort();

            //Convert the match values to substrings
            foreach (var value in resValues)
            {
                var printLength = Math.Min(search.Length + extraCharAmount, Text.Length - value);
                res.Add($"Index: {value}, Text: {Text.Substring(value, printLength)}");
            }

            return res;
        }

        //Helper method to find all values from children of a node
        private void FindValuesOfChildren(ICollection<int> values, Node node, int maxResultAmount)
        {
            if (node.GetType() == typeof(KeyNode))
            {
                values.Add(node.Value);
            }

            foreach (var child in node.Children)
            {
                if (values.Count > maxResultAmount) break; //Lower number for better performance
                FindValuesOfChildren(values, child, maxResultAmount);
            }
        }
    }
}