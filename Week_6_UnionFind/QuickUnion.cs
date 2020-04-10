using System.Runtime.InteropServices;

namespace Week_6_UnionFind
{
    public class QuickUnion : IUnionFind
    {
        private int count;
        private int[] pointsets;

        public QuickUnion(int count)
        {
            this.count = count;
            pointsets = new int[count];
            for (int i = 0; i < count; i++)
            {
                pointsets[i] = i;
            }
        }

        public void Union(int p, int q)
        {
            var rootP = Find(p);
            var rootQ = Find(q);
            if (rootP == rootQ) return;
            pointsets[rootP] = rootQ;
            count--;
        }

        public int Find(int p)
        {
            while (p != pointsets[p])
            {
                p = pointsets[p];
            }
            return p;
        }

        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        public int Count()
        {
            return count;

        }
    }
}