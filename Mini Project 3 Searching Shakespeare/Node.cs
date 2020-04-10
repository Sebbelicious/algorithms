using System;
using System.Collections.Generic;
using System.Linq;

namespace Mini_Project_3_Searching_Shakespeare
{
    public class Node : INode
    {
        public LinkedList<IChildNode> Children = new LinkedList<IChildNode>();

        public void Add(string text, int start, int end, int value)
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
                var count = CountMatchingChars(text, start, end, node.Start);
                SplitKeyNodeAndAddBothToNewLinkedNode((KeyNode) node, start, end, value, count);
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
            //Find child node with matching start char if exists
            var node = Children.FirstOrDefault(child => text[child.Start].Equals(search[0]));

            //Will return parent node of results or null if no there's search matches
            return node?.Locate(text, search);
        }

        public void FindResultValues(ICollection<int> values, int maxResultAmount)
        {
            foreach (var child in Children)
            {
                child.FindResultValues(values, maxResultAmount);
                if (values.Count > maxResultAmount) break; //Lower number for better performance
            }
        }
    }
}