using System;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteFibonacci(20);
        }

        static void WriteFibonacci(int n)
        {
            int[] array = new int[n];
            array[0] = 0;
            array[1] = 1;
            if (n > 2) 
            {
                for (int i = 2; i < n; i++)
                {
                    array[i] = array[i - 1] + array[i - 2];
                }
            }
            string fibonacci = "";
            for (int i = 0; i < n; i++)
            {
                fibonacci += array[i].ToString() + ", ";
            }
            Console.WriteLine(fibonacci);
        }
    }
}
