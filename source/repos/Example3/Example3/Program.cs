using System;

namespace Example3
{
    class Program
    {
        static void Main(string[] args)
        {
            FindMultiples(-20);
        }

        static void FindMultiples (int n)
        {
            if (n == 0)
            {
                Console.WriteLine("Nice One");
            } else if (n > 0)
            {
                for (int i = 1; i <= n; i++)
                {
                    int a = i % 3;
                    int b = i % 5;
                    if (a == 0 && b == 0)
                    {
                        Console.WriteLine("FizzBuzz");
                    }
                    else if (a == 0)
                    {
                        Console.WriteLine("Fizz");
                    }
                    else if (b == 0)
                    {
                        Console.WriteLine("Buzz");
                    }
                    else
                    {
                        Console.WriteLine(i.ToString());
                    }
                }
            } else if (n < 0)
            {
                for (int i = -1; i >= n; i--)
                {
                    int a = i % 3;
                    int b = i % 5;
                    if (a == 0 && b == 0)
                    {
                        Console.WriteLine("FizzBuzz");
                    }
                    else if (a == 0)
                    {
                        Console.WriteLine("Fizz");
                    }
                    else if (b == 0)
                    {
                        Console.WriteLine("Buzz");
                    }
                    else
                    {
                        Console.WriteLine(i.ToString());
                    }
                }
            }
            
            
        }
    }
}
