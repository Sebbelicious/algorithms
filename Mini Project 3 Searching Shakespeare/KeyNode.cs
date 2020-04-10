using System.Collections.Generic;

namespace Mini_Project_3_Searching_Shakespeare
{
    public class KeyNode : IChildNode
    {
        //Start = startindex (for searching)
        public int Start { get; }

        //ENd = endindex (for searching)
        public int End { get; }

        //Value = startindex of the suffix it represents
        public readonly int Value;

        public KeyNode(int start, int end, int value)
        {
            Start = start;
            End = end;
            Value = value;
        }

        public void Add(string text, int start, int end, int value)
        {
            //The recursion stops here
        }

        public IChildNode? Locate(string text, string search)
        {
            var nodeLength = End - Start;
            //If search is shorter than or equal to node and if search is a substring of node return this, else return null
            return search.Length <= nodeLength && text.Substring(Start, search.Length).Equals(search) ? this : null;
        }

        public void FindResultValues(ICollection<int> values, int maxResultAmount)
        {
            values.Add(Value);
        }
    }
}