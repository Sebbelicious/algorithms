using System.Collections.Generic;
using System.Linq;

namespace Mini_Project_3_Searching_Shakespeare
{
    public class LinkedNode : IChildNode
    {
        public int Start { get; }
        private int End { get; set; }
        public LinkedList<IChildNode> Children = new LinkedList<IChildNode>();

        public LinkedNode(int start, int end)
        {
            Start = start;
            End = end;
        }

        public void Add(string text, int start, int end, int value)
        {
            //key is shorter than nodeKey
            if (end - start < End - Start)
            {
                var count = CountMatchingChars(text, start, end, Start);
                SplitLinkedNodeAndAddNewKeyNode(start, end, value, count);
            }
            //nodekey is longer than key
            else
            {
                var count = CountMatchingChars(text, Start, End, start);

                if (Start + count < End) // Only part of the nodetext is in current suffix
                {
                    SplitLinkedNodeAndAddNewKeyNode(start, end, value, count);
                }
                //The full nodetext is in current suffix
                else
                {
                    start += count;
                    //nodetext matches current suffix (and stops IndexOutOfRangeException in the else)
                    if (start == end)
                    {
                        Children.AddLast(new KeyNode(start, end, value));
                    }
                    //continue add on child
                    else
                    {
                        //Find child with matching starting char if exists
                        var node = Children.FirstOrDefault(child => text[child.Start].Equals(text[start]));

                        //Node is null, if there was no match
                        if (node == null)
                        {
                            Children.AddLast(new KeyNode(start, end, value));
                        }
                        else if (node.GetType() == typeof(LinkedNode))
                        {
                            node.Add(text, start, end, value);
                        }
                        else //Node is KeyNode
                        {
                            SplitKeyNodeAndAddBothToNewLinkedNode(text, (KeyNode) node, start, end, value);
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
            //Splits "this" into two LinkedNodes and adds new KeyNode for current suffix
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

        private void SplitKeyNodeAndAddBothToNewLinkedNode(string text, KeyNode node, int start, int end, int value)
        {
            //Count matching chars
            var count = CountMatchingChars(text, start, end, node.Start);

            //Replace node with new LinkedNode with lenth og the count of charmatches and add two KeyNodes.
            //One for node and one for the current value
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
            //If search is shorter than or equal to node
            if (search.Length <= nodeLength)
            {
                //if search is a substring of node return this, else return null
                return text.Substring(Start, search.Length).Equals(search) ? this : null;
            }

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