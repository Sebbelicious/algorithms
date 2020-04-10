using System.Collections.Generic;
using System.Linq;

namespace Mini_Project_3_Searching_Shakespeare
{
    public class LinkedNode : IChildNode
    {
        public int Start { get; set; }
        public int End { get; set; }
        public LinkedList<IChildNode> Children = new LinkedList<IChildNode>();

        public LinkedNode(int start, int end)
        {
            Start = start;
            End = end;
        }

        public void Add(string text, int start, int end, int value)
        {
            if (end - start < End - Start) //key is shorter than nodeKey
            {
                var count = CountMatchingChars(text, start, end, Start);
                SplitLinkedNodeAndAddNewKeyNode(start, end, value, count);
            }
            else //nodekey is longer than key
            {
                var count = CountMatchingChars(text, Start, End, start);

                if (Start + count < End) // Only part of the nodekey is in key
                {
                    SplitLinkedNodeAndAddNewKeyNode(start, end, value, count);
                }
                else //The full nodekey is in key
                {
                    start += count;
                    if (start == end) //nodekey matches key
                    {
                        Children.AddLast(new KeyNode(start, end, value));
                    }
                    else //continue add on child
                    {
                        //Find child with matching starting char if exists
                        var node = Children.FirstOrDefault(child => text[child.Start].Equals(text[start]));

                        //Node is null, if there was no match
                        if (node == null)
                        {
                            Children.AddLast(new KeyNode(start, end, value));
                        }
                        else if (node.GetType() == typeof(LinkedNode)) //Node is LinkedNode if true
                        {
                            node.Add(text, start, end, value);
                        }
                        else //Node is KeyNode
                        {
                            count = CountMatchingChars(text, start, end, node.Start);
                            SplitKeyNodeAndAddBothToNewLinkedNode((KeyNode) node, start, end, value, count);
                        }
                    }
                }
            }
        }


        private static int CountMatchingChars(string text, int intervalStart, int intervalEnd, int otherStart)
        {
            var length = intervalEnd - intervalStart;
            for (var i = 0; i < length; i++)
            {
                if (!text[intervalStart + i].Equals(text[otherStart + i]))
                {
                    return i;
                }
            }

            return length;
        }

        private void SplitLinkedNodeAndAddNewKeyNode(int start, int end, int value, int matchingCharCount)
        {
            var newLinkedNode = new LinkedNode(Start + matchingCharCount, End)
            {
                Children = Children
            };
            Children = new LinkedList<IChildNode>();
            Children.AddLast(newLinkedNode);
            End = Start + matchingCharCount;
            var newKeyNode = new KeyNode(start + matchingCharCount, end, value);
            Children.AddLast(newKeyNode);
        }

        private void SplitKeyNodeAndAddBothToNewLinkedNode(KeyNode node, int start, int end, int value, int count)
        {
            var newLinkedNode = new LinkedNode(node.Start, node.Start + count);
            var newKeyNode1 = new KeyNode(node.Start + count, node.End, node.Value);
            var newKeyNode2 = new KeyNode(start + count, end, value);
            newLinkedNode.Children.AddLast(newKeyNode1);
            newLinkedNode.Children.AddLast(newKeyNode2);
            Children.Remove(node);
            Children.AddFirst(newLinkedNode);
        }

        public IChildNode? Locate(string text, string search)
        {
            var nodeLength = End - Start;
            //If search is shorter than or equal to node, if search is a substring of node return this, else return null
            if (search.Length <= nodeLength) return text.Substring(Start, search.Length).Equals(search) ? this : null;

            //Else Search is longer than node. Check if node is substring of search
            if (!search.StartsWith(text.Substring(Start, nodeLength))) return null;

            //Else we need to go deeper:
            //Find child node with matching start char if exists
            var node = Children.FirstOrDefault(child => text[child.Start].Equals(search[nodeLength]));

            //Will return parent node of results or null if no there's search matches
            return node?.Locate(text, search.Substring(nodeLength));
        }

        public void FindResultValues(ICollection<int> values, int maxResultAmount)
        {
            foreach (var child in Children)
            {
                if (values.Count > maxResultAmount) break; //Lower number for better performance
                child.FindResultValues(values, maxResultAmount);
            }
        }
    }
}