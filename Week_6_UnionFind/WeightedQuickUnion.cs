namespace Week_6_UnionFind
{
    public class WeightedQuickUnion : IUnionFind
    {
        int count;
        int[] pointsets;
        int[] sizes;

        public WeightedQuickUnion(int count)
        {
            this.count = count;
            pointsets = new int[count];
            sizes = new int[count];
            for (int i = 0; i < count; i++)
            {
                pointsets[i] = i;
                sizes[i] = 1;
            }
        }

        public void Union(int p, int q)
        {
            var rootP = Find(p);
            var rootQ = Find(q);
            
            if (rootP == rootQ) return;
            int sizeRootP = sizes[rootP];
            int sizeRootQ = sizes[rootQ];
            
            if (sizeRootP > sizeRootQ)
            {
                pointsets[rootP] = rootQ;
                sizes[rootQ] += sizes[rootP];
            } else 
            {
                pointsets[rootQ] = rootP;
                sizes[rootP] += sizes[rootQ];
            }
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