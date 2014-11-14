using System;

namespace Practice.Algorithm.Finding
{
    public class SimpleSearch<T, T1>
    {
        private Func<T, T1, int> _compareFunc;

        public SimpleSearch(Func<T, T1, int> compareFunc)
        {
            _compareFunc = compareFunc;
        } 

        public bool Search(T[] source, T1 key, out int index)
        {
            for (int i = 0; i < source.Length; i++)
            {
                index = i;
                int compare = _compareFunc(source[i], key);
                if (compare == 0)
                {
                    return true;
                }

                if (compare > 0)
                {
                    return false;
                }
            }

            index = source.Length;
            return false;
        }
    }
}
