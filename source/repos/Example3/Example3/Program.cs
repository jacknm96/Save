using System;

namespace Example3
{
    class Program
    {
        static void Main(string[] args)
        {
            FindMultiples(-30);
        }

        static void FindMultiples (int n)
        {
            if (n == 0) // no loop necessary, prints '0' as it is not a multiple of 3 or 5
            {
                Console.WriteLine("0");
            } else if (n > 0)
            {
                for (int i = 1; i <= n; i++)
                {
                    bool a = i % 3 == 0; // stores values as boolean, true if divisible by 3
                    bool b = i % 5 == 0; // stores values as boolean, true if divisible by 5
                    if (a)
                    {
                        Console.Write("Fizz");
                    }
                    if (b)
                    {
                        Console.Write("Buzz");
                    }
                    if (!a && !b)
                    {
                        Console.Write(i.ToString());
                    }
                    Console.WriteLine();
                }
            } else // same as previous loop, but accounts for negative numbers
            {
                for (int i = -1; i >= n; i--)
                {
                    bool a = i % 3 == 0; // stores values as boolean, true if divisible by 3
                    bool b = i % 5 == 0; // stores values as boolean, true if divisible by 5
                    if (a)
                    {
                        Console.Write("Fizz");
                    }
                    if (b)
                    {
                        Console.Write("Buzz");
                    }
                    if (!a && !b)
                    {
                        Console.Write(i.ToString());
                    }
                    Console.WriteLine();
                }
            } 
        }
    }
}
