using System;

namespace ArrayExercise2
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("How long would you like your array?");
            int a;
            string s = Console.ReadLine();
            while (!int.TryParse(s, out a) || a <= 0)
            {
                Console.WriteLine("Please insert positive ints only please.");
                s = Console.ReadLine();
            }
            Console.WriteLine();
            int[] arr = new int[a];
            Console.WriteLine("What is the max value in our array?");
            s = Console.ReadLine();
            while (!int.TryParse(s, out a) || a <= 0)
            {
                Console.WriteLine("Please insert positive ints only please.");
                s = Console.ReadLine();
            }
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++) // creates an array of random numbers from 1 to given number
            {
                arr[i] = random.Next(1, a + 1);
            }
            string before = "Before sort: ";
            foreach (int i in arr)
            {
                before += i.ToString() + ", ";
            }
            Console.WriteLine(before);
            int[] newArray = SortArray(arr);
            string sorted = "After sort: ";
            foreach (int i in newArray)
            {
                sorted += i.ToString() + ", ";
            }
            Console.WriteLine(sorted);
            Console.ReadLine();
        }

        static int[] SortArray (int[] arr)
        {
            int[] newArray = arr;
            try
            {
                int i = arr[1];
            } catch (System.Exception e)
            {
                Console.WriteLine("Array too short to sort");
            }
            bool clean = false; // will check to see if the array is sorted yet
            while (!clean) // while array is not sorted
            {
                clean = true; // prime to assume it is sorted
                for (int i = 0; i < arr.Length - 1; i++)
                {
                    if (newArray[i] < newArray[i + 1]) // flips the values in the 2 slots if the 2nd int is greater than the first
                    {
                        int temp = newArray[i];
                        newArray[i] = newArray[i + 1];
                        newArray[i + 1] = temp;
                        clean = false; // reassigns to not sorted
                    }
                }
            }
            return newArray;
        }
    }
}
