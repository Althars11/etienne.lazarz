using System;
using System.Security.AccessControl;

namespace TP4.Properties
{
    public class Misc
    {
        public static String StripSpace(String str)
        {
            bool isnotspace = true;
            int len = str.Length;
            int i = len - 1;
            while (i>0 && isnotspace)
            {
                if (str[i]==' ')
                {
                    str.Remove(i);
                }

                isnotspace = false;
                i--;
            }

            return str;
        }

        public static void Swap(ref int a, ref int b)
        {
            int stocker = b;
            b = a;
            a = stocker;
        }

        public static void Sort(int[] arr)
        {
            int arr_len = arr.Length;
            
            for (int i = 0; i <= arr_len-1; i++)
            {
                for (int j = i+1; j <= arr_len; j++)
                {
                    if (arr[j]<arr[j-1])
                    {
                        int aide = arr[j];
                        arr[j] = arr[j - 1];
                        arr[j - 1] = aide;
                    }
                }
            }
        }

        public static int FiboIter(int n)
        {
            
        }
    }
}