namespace Week_6_UnionFind
{
    public class FirstFind : IUnionFind
    {
        private int[] pointsets;
        private int count;

        public FirstFind(int count)
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
            if (!Connected(p, q))
            {
                for (int i = 0; i < pointsets.Length; i++)
                {
                    var tmp1 = pointsets[p];
                    var tmp2 = pointsets[q];
                    if (pointsets[i] == tmp1)
                    {
                        pointsets[i] = tmp2;
                        count--;
                    }
                }
                
            }
        }

        public int Find(int p)
        {
            return pointsets[p];
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