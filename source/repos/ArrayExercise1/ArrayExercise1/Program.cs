using System;

namespace ArrayExercise1
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = random.Next(1, 30);
            }
            string before = "Your array: ";
            foreach (int i in arr)
            {
                before += i.ToString() + ", ";
            }
            Console.WriteLine(before);
            Console.WriteLine("Total sum = " + SumArray(arr).ToString());
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
