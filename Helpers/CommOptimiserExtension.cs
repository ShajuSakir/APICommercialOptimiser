using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICommercialOptimiser.Helpers
{
    public static class CommOptimiserExtension
    {
        private static Random rng = new Random();
        public static void Shuffle<T>(this T[] array)
        {
            rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n);
                n--;
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];
            //if (index > 0)
            //    Array.Copy(source, 0, dest, 0, index);

            //if (index < source.Length - 1)
            //    Array.Copy(source, index + 1, dest, index, source.Length - index - 1);


            int i = 0;
            int j = 0;
            while (i < source.Length)
            {
                if (i != index)
                {
                    dest[j] = source[i];
                    j++;
                }

                i++;
            }

            return dest;
        }


        public static void Move<T>(this List<T> list, int oldIndex, int newIndex)
        {
            List<T> tempList = new List<T>(list);
            T item = list[oldIndex];
            tempList.RemoveAt(oldIndex);
            list.Clear();
            int j = 0;
            for (int i = 0; i < tempList.Count + 1; i++)
            {
                list.Add(i == newIndex ? item : tempList[j]);
                j += i == newIndex ? 0 : 1;
            }
        }
    }
}
