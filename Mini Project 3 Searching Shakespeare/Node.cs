using System.Collections.Generic;
using System.Linq;

namespace Mini_Project_3_Searching_Shakespeare
{
    public class Node : INode
    {
        private readonly LinkedList<IChildNode> _children = new LinkedList<IChildNode>();

        public void Add(string text, int start, int end, int value)
        {
            //Find child with matching starting char if exists
            var node = _children.FirstOrDefault(child => text[child.Start].Equals(text[start]));

            //node is null, if there was no match
            if (node == null)
            {
                _children.AddLast(new KeyNode(start, end, value));
            }
            else if (node.GetType() == typeof(LinkedNode)) //node is LinkedNode
            {
                node.Add(text, start, end, value);
            }
            else //node is KeyNode
            {
                SplitKeyNodeAndAddBothToNewLinkedNode(text, (KeyNode) node, start, end, value);
            }
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
            _children.Remove(node);
            _children.AddFirst(newLinkedNode);
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


        public IChildNode? Locate(string text, string search)
        {
            //Find child node with matching start char if exists
            var node = _children.FirstOrDefault(child => text[child.Start].Equals(search[0]));

            //Will return parent node of results or null if no there's search matches
            return node?.Locate(text, search);
        }

        public void FindResultValues(ICollection<int> values, int maxResultAmount)
        {
            foreach (var child in _children)
            {
                child.FindResultValues(values, maxResultAmount);
                if (values.Count > maxResultAmount) break; //Lower number for better performance
            }
        }
    }
}