using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2048.Src
{
    class ArrayHelper
    {
        public static void putColumn(IEnumerable<int> column, int[,] array, int index)
        {
            for (int i = 0; i < column.Count(); i++)
            {
                array[i, index] = column.ElementAt(i);
            }
        }

        public static int[] getColumn(int[,] array, int index)
        {
            int length = array.GetLength(0);
            int[] slice = new int[length];
            for (int i = 0; i < length; i++)
            {
                slice[i] = array[i, index];
            }
            return slice;
        }

        public static void putRow(IEnumerable<int> row, int[,] array, int index)
        {
            for (int i = 0; i < row.Count(); i++)
            {
                array[index, i] = row.ElementAt(i);
            }
        }

        public static int[] getRow(int[,] array, int index)
        {
            int length = array.GetLength(0);
            int[] slice = new int[length];
            for (int i = 0; i < length; i++)
            {
                slice[i] = array[index, i];
            }
            return slice;
        }

        public static int[] collapseToFirst(IEnumerable<int> array)
        {
            int length = array.Count();
            int[] collapsedArray = new int[length];
            int nextSquare = 0;
            for (int i = 0; i < length; i++)
            {
                int found = 0;
                for (int j = nextSquare; j < length; j++)
                {
                    int value = array.ElementAt(j);
                    if (value == 0) continue;
                    if (found != 0)
                    {
                        if (value == found)
                        {
                            found = 2 * found;
                            nextSquare = j + 1;
                            break;
                        }
                        else if (j != 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        found = value;
                        nextSquare = j + 1;
                    }
                }
                collapsedArray[i] = found;
            }
            return collapsedArray;
        }

        public static IEnumerable<int> collapseToLast(int[] array)
        {
            return collapseToFirst(array.Reverse()).Reverse();
        }
    }
}
