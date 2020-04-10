using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Assignment3_Balanced_Search_Tree
{
    public class RedBlackBST<Key, Value> where Key : IComparable<Key>
    {
        
        private Node Root;
        private bool _isRed = true; 
        
        private class Node
        {
            public Key Key; 
            public Value Value;
            public bool IsRed;
            public int N;
            public Node Left;
            public Node Right;

            public Node(Key key, Value value, int n, bool isRed)
            {
                Key = key;
                Value = value;
                N = n;
                IsRed = isRed;
                Left = null;
                Right = null;
            }
        }
        
        private bool IsRed(Node x)
        {
            return x != null && x.IsRed;
        }
        
        private Node RotateLeft(Node h) 
        {   var x = h.Right;   
            h.Right = x.Left;   
            x.Left = h;   
            x.IsRed = h.IsRed;   
            h.IsRed = _isRed;   
            x.N = h.N;   
            h.N = 1 + Size(h.Left)+Size(h.Right);   
            return x;
        }

        private Node RotateRight(Node h)
        {
            Node x = h.Left;   
            h.Left = x.Right;   
            x.Right = h;   
            x.IsRed = h.IsRed;   
            h.IsRed = _isRed;   
            x.N = h.N;   
            h.N = 1+Size(h.Left)+Size(h.Right);   
            return x;
        }

        private void FlipColors(Node h)
        {
            h.IsRed = _isRed;   
            h.Left.IsRed = !_isRed;   
            h.Right.IsRed = !_isRed;
        }

        public int Size()
        {
            return Size(Root);
        }

        private int Size(Node x)
        {
            return x?.N ?? 0;
        }
        
        public void Put(Key key, Value val)   
        {  // Search for key. Update value if found; grow table if new.
           Root = Put(Root, key, val);      
           Root.IsRed = !_isRed;   
        }   
        
        private Node Put(Node h, Key key, Value val)   
        {
            if (h == null) // Do standard insert, with red link to parent.
            {
                return new Node(key, val, 1, _isRed);
            }

            var cmp = key.CompareTo(h.Key);      
            if (cmp < 0) h.Left  = Put(h.Left,  key, val);      
            else if (cmp > 0) h.Right = Put(h.Right, key, val);      
            else h.Value = val;      
            if (IsRed(h.Right) && !IsRed(h.Left)) h = RotateLeft(h);      
            if (IsRed(h.Left) && IsRed(h.Left.Left)) h = RotateRight(h);      
            if (IsRed(h.Left) && IsRed(h.Right)) FlipColors(h);      
            h.N = Size(h.Left) + Size(h.Right) + 1;      
            return h;   
        }

        public List<string> PrintTreeToArray()
        {
            var keys = new List<string>();

            if (Root == null) return keys;
            if (Root.Left != null) PrintTreeToArray(keys, Root.Left);
            var color = Root.IsRed ? "Red" : "Black"; 
            keys.Add(Root.Key + color);
            if (Root.Right != null)PrintTreeToArray(keys, Root.Right);

            return keys;
        }

        private Node PrintTreeToArray(List<string> keys, Node h)
        {
            if (h.Left != null) PrintTreeToArray(keys, h.Left);
            var color = h.IsRed ? "Red" : "Black"; 
            keys.Add(h.Key + color);
            if (h.Right != null) PrintTreeToArray(keys, h.Right);
            
            return h;
        }

        public List<string> PrintTree()
        {
            var tree = new List<string>();
            var layer = 0;
            
            if (Root == null) return tree;
            var color = Root.IsRed ? "R" : "B";
            tree.Add(string.Empty + Root.Key + color);
            
            if (Root.Left != null) PrintTree(tree, Root.Left, layer+1);
            if (Root.Right != null)PrintTree(tree, Root.Right, layer+1);

            return tree;
        }

        private Node PrintTree(List<string> tree, Node h, int layer)
        {
            var color = h.IsRed ? "R" : "B";
            if (layer >= tree.Count)
                tree.Add(string.Empty + h.Key + color); 
            else 
                tree[layer] += " " + h.Key + color;
            if (h.Left != null) PrintTree(tree, h.Left, layer+1);
            if (h.Right != null) PrintTree(tree, h.Right, layer+1);
            
            return h;
        }
        
        public bool IsBalanced() { 
            int black = 0;     // number of black links on path from root to min
            Node x = Root;
            while (x != null) {
                if (!IsRed(x)) black++;
                x = x.Left;
            }
            return IsBalanced(Root, black);
        }

        // does every path from the root to a leaf have the given number of black links?
        private bool IsBalanced(Node x, int black) {
            if (x == null) return black == 0;
            if (!IsRed(x)) black--;
            return IsBalanced(x.Left, black) && IsBalanced(x.Right, black);
        } 
        
    }
}