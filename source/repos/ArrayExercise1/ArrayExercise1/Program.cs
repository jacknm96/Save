using System;

namespace ArrayExercise1
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            int[] arr = new int[10];
            for (int i = 0; i < arr.Length; i++) //fills the array with random numbers from 1 to 29
            {
                arr[i] = random.Next(1, 30);
            }
            string before = "Your array: ";
            foreach (int i in arr) // prints out the array so the user can verify the numbers
            {
                before += i.ToString() + ", ";
            }
            Console.WriteLine(before);
            Console.WriteLine("Total sum = " + SumArray(arr).ToString()); // prints out the total
        }

        // takes in an int array and returns the total sum of all the ints
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
