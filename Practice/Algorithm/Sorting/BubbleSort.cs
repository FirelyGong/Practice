using System;
using System.Linq;

namespace Practice.Algorithm.Sorting
{
    public class BubbleSort<T>
    {
        public T[] Sort(T[] elements, Func<T, T, bool> compareFunc)
        {
            T[] copy = new T[elements.Count()];
            elements.CopyTo(copy, 0);
            for (int i = 0; i < copy.Count(); i++)
            {
                for (int j = i; j < copy.Count(); j++)
                {
                    if (!compareFunc(copy[i], copy[j]))
                    {
                        T temp = copy[i];
                        copy[i] = copy[j];
                        copy[j] = temp;
                    }
                }
            }

            return copy;
        }
    }
}
