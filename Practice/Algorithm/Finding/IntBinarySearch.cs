namespace Practice.Algorithm.Finding
{
    public class IntBinarySearch : BinarySearch<int, int>
    {
        public IntBinarySearch()
            : base(CompareInt)
        {
        }
        
        private static int CompareInt(int i, int k)
        {
            if (i == k)
            {
                return 0;
            }
            else
            {
                if (i > k)
                {
                    return 1;
                }

                return -1;
            }
        }
    }
}
