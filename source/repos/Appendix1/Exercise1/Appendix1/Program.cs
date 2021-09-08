using System;

namespace Appendix1
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = AddNumbers(38.36f, 78.22f);
            Console.WriteLine(sum.ToString());
            Console.ReadLine();
        }

        static int AddNumbers(float a, float b)
        {
            return (int)MathF.Floor(a + b);
        }
    }
}
