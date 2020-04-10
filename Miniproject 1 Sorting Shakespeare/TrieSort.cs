namespace Miniproject_1_Sorting_Shakespeare
{
    public static class TrieSort
    {
        //Index used when using TrieSort (saves value through the recursion)
        private static int _trieIdx;
        
        public static void Run(string[] words)
        {
            _trieIdx = 0;
            var root = new Letter(" ");

            foreach (var word in words)
            {
                root.CreateOrUpdateChild(word);
            }
            TrieToArray(words, root);
        }

        private static void TrieToArray(string[] words, Letter letter, string word = "")
        {
            if (!letter.Value.Equals(' '))
            {
                word += letter.Value;
                while (letter.Count > 0)
                {
                    words[_trieIdx] = word;
                    _trieIdx++;
                    letter.Count--;
                }
            }
            
            foreach (var child in letter.Children)
            {
                if (child != null)
                {
                    TrieToArray(words, child, word);
                }
            }
        }

        private class Letter
        {
            public readonly Letter[] Children = new Letter[28];
            public int Count;
            public readonly char Value;

            public Letter(string rest)
            {
                Value = rest[0]; 
                if (rest.Length > 1)
                {
                    CreateOrUpdateChild(rest.Substring(1));
                }
                else
                {
                    Count++;
                }
            }

            public void CreateOrUpdateChild(string rest)
            {
                var curr = rest[0];
                var i = curr switch
                {
                    '\'' => 0,
                    '-' => 1,
                    _ => curr - 'a' + 2
                };
                var child = Children[i];

                if (child is null)
                {
                    child = new Letter(rest);
                    Children[i] = child;
                }
                else if (rest.Length > 1)
                {
                    child.CreateOrUpdateChild(rest.Substring(1));
                }
                else if (rest.Length == 1)
                {
                    child.Count++;
                }
                
            }
            
            
        }
    }
}