using System;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        float AddTwoNumbers(float a, float b)
        {
            return MathF.Floor(a + b);
        }
    }
}
