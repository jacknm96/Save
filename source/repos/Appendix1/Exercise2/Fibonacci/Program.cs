using System;

namespace Fibonacci
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many digits of Fibonacci would you like?");
            int a;
            string s = Console.ReadLine();
            while (!int.TryParse(s, out a) || a <= 0)
            {
                Console.WriteLine("Please give a positive integer.");
                s = Console.ReadLine();
            }
            WriteFibonacci(a); // input value equals how many elements of Fibonacci
            Console.ReadLine();
        }

        static void WriteFibonacci(int n)
        {
            if (n == 1) // 1 digit of Fibonacci will always be 0
            {
                Console.WriteLine("0");
            }
            else
            {
                int[] array = new int[n]; // keeps track of our units of Fibonacci
                array[0] = 0;
                array[1] = 1;
                string fibonacci = "0, 1";
                if (n > 2)
                {
                    for (int i = 2; i < n; i++)
                    {
                        array[i] = array[i - 1] + array[i - 2];
                        fibonacci += ", " + array[i].ToString();
                    }
                }
                Console.WriteLine(fibonacci);
            }
        }
    }
}
