using System;

namespace ArrayExercise1
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("How long would you like your array?");
            int a;
            string s = Console.ReadLine();
            while (!int.TryParse(s, out a))
            {
                Console.WriteLine("Please insert ints only please.");
                s = Console.ReadLine();
            }
            Console.WriteLine();
            int[] arr = new int[a];
            Console.WriteLine("What is the max value in our array?");
            s = Console.ReadLine();
            while (!int.TryParse(s, out a))
            {
                Console.WriteLine("Please insert ints only please.");
                s = Console.ReadLine();
            }
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(1, a + 1);
            }
            string before = "Your array: ";
            foreach (int i in arr)
            {
                before += i.ToString() + ", ";
            }
            Console.WriteLine(before);
            Console.WriteLine();
            Console.WriteLine("Total sum = " + SumArray(arr).ToString());
            Console.ReadLine();
        }

        static int SumArray(int[] arr)
        {
            int value = 0;
            foreach (int i in arr) // iterates through array
            {
                value += i; // adds each value to the total
            }
            return value;
        }
    }
}
