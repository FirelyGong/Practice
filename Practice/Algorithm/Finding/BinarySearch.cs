using System;

namespace Practice.Algorithm.Finding
{
    public class BinarySearch<T, T1>
    {
        private Func<T, T1, int> _compareFunc;

        public BinarySearch(Func<T, T1, int> compareFunc)
        {
            _compareFunc = compareFunc;
        }

        public bool Search(T[] source, T1 key, out int index)
        {
            return Search(source, key, 0, source.Length - 1, out index);
        }

        private bool Search(T[] source, T1 key, int start, int end, out int index)
        {
            int middle = (start + end) / 2;
            if (end < start)
            {
                if (end < 0)
                {
                    index = 0;
                }
                else
                {
                    if (_compareFunc(source[end], key) > 0)
                    {
                        index = end;
                    }
                    else
                    {
                        index = start;
                    }

                }

                return false;
            }

            int compare = _compareFunc(source[middle], key);
            if (compare == 0)
            {
                index = middle;
                return true;
            }
            else
            {
                if (compare < 0)
                {
                    return Search(source, key, middle + 1, end, out index);
                }
                else
                {
                    return Search(source, key, start, middle - 1, out index);
                }
            }
        }
    }
}
