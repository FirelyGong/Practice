using System;
using System.Linq;

namespace Practice.Algorithm.Sorting
{
    public class QuickSort<T>
    {
        public T[] Sort(T[] elements, Func<T, T, bool> compareFunc)
        {
            T[] copy = new T[elements.Count()];
            elements.CopyTo(copy, 0);
            Sort(copy, compareFunc, 0, elements.Count() - 1);
            return copy;
        }

        private void Sort(T[] elements, Func<T, T, bool> compareFunc, int start, int end)
        {
            if (start < end)
            {
                T current = elements[start];
                int i = start;
                int j = end;

                while (i < j)
                {
                    while (i < j)
                    {
                        if (compareFunc(current, elements[j]))
                        {
                            j--;
                        }
                        else
                        {
                            elements[i] = elements[j];
                            break;
                        }
                    }

                    while (i < j)
                    {
                        if (!compareFunc(current, elements[i]))
                        {
                            i++;
                        }
                        else
                        {
                            elements[j] = elements[i];
                            break;
                        }
                    }
                }

                elements[i] = current;
                Sort(elements, compareFunc, start, i - 1);
                Sort(elements, compareFunc, i + 1, end);
            }
        }
    }
}
