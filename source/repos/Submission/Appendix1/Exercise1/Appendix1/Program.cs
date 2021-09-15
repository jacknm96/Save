using System;

namespace Appendix1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What numbers would you like to add?");
            Console.WriteLine("First number:");
            float a;
            float b;
            string s = Console.ReadLine();
            while (!float.TryParse(s, out a)) // loops until the user inputs a valid float number
            {
                Console.WriteLine("Please insert floats only please.");
                s = Console.ReadLine();
            }
            Console.WriteLine("Second number:");
            string p = Console.ReadLine();
            while (!float.TryParse(p, out b)) // loops until the user inputs a valid float number
            {
                Console.WriteLine("Please insert floats only please.");
                p = Console.ReadLine();
            }
            int sum = AddNumbers(a, b);
            Console.WriteLine();
            Console.WriteLine("Total:");
            Console.WriteLine(sum.ToString());
            Console.ReadLine();
        }

        // takes 2 floats and returns their sum as an int
        static int AddNumbers(float a, float b)
        {
            return (int)MathF.Floor(a + b);
        }
    }
}
