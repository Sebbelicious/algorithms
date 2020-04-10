using System;
using System.Collections.Generic;
using System.Linq;

namespace Mini_Project_3_Searching_Shakespeare
{
    public abstract class Node
    {
        public Key Key;
        public int Value;
        public LinkedList<Node> Children = new LinkedList<Node>();

        protected Node(Key key, int value)
        {
            Key = key;
            Value = value;
        }

        public virtual void Add(string text, Key key, int value)
        {
            //Find child with matching starting char if exists
            var node = Children.FirstOrDefault(child => text[child.Key.Start].Equals(text[key.Start]));

            //Node is null, if there was no match
            if (node == null)
            {
                Children.AddLast(new KeyNode(key, value));
            }
            else if (node.GetType() == typeof(LinkedNode)) //Node is LinkedNode if true
            {
                if (key.End - key.Start >= node.Key.End - node.Key.Start) //key is longer than or equals nodekey
                {
                    var count = CountMatchingChars(text, node.Key.Start, node.Key.End, key.Start);

                    if (node.Key.Start + count >= node.Key.End) //The full nodekey is in key
                    {
                        key.Start += count;
                        if (key.Start == key.End) //nodekey matches key
                        {
                            node.Children.AddLast(new KeyNode(key, value));
                        }
                        else //continue add on child
                        {
                            node.Add(text, key, value);
                        }
                    }
                    else // Only part of the nodekey is in key
                    {
                        SplitLinkedNodeAndAddNewKeyNode(node, key, value, count);
                    }
                }
                else //nodekey is longer than key
                {
                    var count = CountMatchingChars(text, key.Start, key.End, node.Key.Start);
                    SplitLinkedNodeAndAddNewKeyNode(node, key, value, count);
                }
            }
            else //Node is KeyNode
            {
                var count = CountMatchingChars(text, key.Start, key.End, node.Key.Start);
                SplitKeyNodeAndAddBothToNewLinkedNode(node, key, value, count);
            }
        }


        protected virtual int CountMatchingChars(string text, int intervalStart, int intervalEnd, int otherStart)
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

        private void SplitLinkedNodeAndAddNewKeyNode(Node node, Key key, int value, int matchingCharCount)
        {
            var newLinkedNode = new LinkedNode(new Key(node.Key.Start + matchingCharCount, node.Key.End),
                node.Key.Start + matchingCharCount)
            {
                Children = node.Children
            };
            node.Children = new LinkedList<Node>();
            node.Children.AddLast(newLinkedNode);
            node.Key.End = node.Key.Start + matchingCharCount;
            var newKeyNode = new KeyNode(new Key(key.Start + matchingCharCount, key.End), value);
            node.Children.AddLast(newKeyNode);
        }

        private void SplitKeyNodeAndAddBothToNewLinkedNode(Node node, Key key, int value, int count)
        {
            var newLinkedNode = new LinkedNode(new Key(node.Key.Start, node.Key.Start + count), node.Key.Start);
            var newKeyNode1 = new KeyNode(new Key(node.Key.Start + count, node.Key.End), node.Value);
            var newKeyNode2 = new KeyNode(new Key(key.Start + count, key.End), value);
            newLinkedNode.Children.AddLast(newKeyNode1);
            newLinkedNode.Children.AddLast(newKeyNode2);
            Children.Remove(node);
            Children.AddFirst(newLinkedNode);
        }

        public virtual Node Locate(string text, string search)
        {
            //Find child node with matching start char if exists
            var node = Children.FirstOrDefault(child => text[child.Key.Start].Equals(search[0]));

            //No search matches
            if (node == null) return null;

            var nodeLength = node.Key.End - node.Key.Start;
            //search is longer than node
            if (search.Length > nodeLength)
            {
                //Check if node is substring of search
                if (text.Substring(node.Key.Start, nodeLength).Equals(search.Substring(0, nodeLength)))
                {
                    //We need to go further in
                    return node.Locate(text, search.Substring(nodeLength));
                }
            }
            //Search is shorter than node and search matches start of node
            else if (text.Substring(node.Key.Start, search.Length).Equals(search))
            {
                //The result node is found and returned
                return node;
            }

            //The search had no matches
            return null;
        }
    }
}