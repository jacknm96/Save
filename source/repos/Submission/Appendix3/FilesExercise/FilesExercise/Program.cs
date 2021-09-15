using System;
using System.IO;
using System.Collections.Generic;

namespace FilesExercise
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("What is the name of the text file you would like to sort?");
            ReadFile(Console.ReadLine());
            Console.ReadLine();
        }

        static void ReadFile(string name)
        {
            StreamReader reader; //set up our reader from our txt file
            bool temp = true;
            int iterations = 0;
            while (temp) // loops until user inputs a correct file
            {
                temp = false;
                try
                {
                    reader = new StreamReader(name);
                }
                catch (Exception e)
                {
                    if (iterations >= 5) // if user cannot input file name, executes to avoid infinite loop
                    {
                        Console.WriteLine("Too many attempts made. Executing program");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Cannot find that txt file. Please input a new file name");
                        name = Console.ReadLine();
                        temp = true;
                    }
                }
                iterations++;
            }
            reader = new StreamReader(name);
            int size = int.Parse(reader.ReadLine()); //first line in txt file will give us how many words total
            string[] words = new string[size];
            for (int i = 0; i < size; i++) // add words from txt file into array
            {
                words[i] = reader.ReadLine();
            }
            reader.Close();
            CocktailShakerSort(words);
            StreamWriter writer = new StreamWriter("output.txt"); // after sorting, output our sorted words into txt file
            foreach (string word in words)
            {
                writer.WriteLine(word);
            }
            writer.Close();
        }

        // takes an array of strings as an argument and sorts them alphabetically
        static void CocktailShakerSort(string[] array)
        {
            if (array.Length < 2) // arrays of size 1 are already sorted
            {
                return;
            }
            bool sorted = false;
            int j = 1;
            while (!sorted)
            {
                sorted = true; //primes our bool so that, if we iterate through array and don't perform any swaps, we are sorted
                int switchIndex = 0; //keeps track of our last sort so that on our return loop we can start there
                for (int i = 0; i < array.Length - j; i++)
                {
                    if (FirstGreater(array[i], array[i + 1]))
                    {
                        switchIndex = i;
                        string temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        sorted = false;
                    }
                }
                if (sorted) //returns if we went through previous loop without sorting anything, to save time
                {
                    return;
                }
                for (int i = switchIndex; i > j; i--)
                {
                    if (FirstGreater(array[i - 1], array[i]))
                    {
                        string temp = array[i];
                        array[i] = array[i - 1];
                        array[i - 1] = temp;
                        sorted = false;
                    }
                }
                j++;
            }
        }

        // takes 2 strings as arguments and iterates through them, comparing individual characters. returns true if the
        // first string is alphabetically greater than the second, otherwise returns false;
        static bool FirstGreater(string first, string second)
        {
            if (first.Length <= second.Length) //iterate through the string with fewer letters to avoid out of bound exceptions
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (first[i] > second[i]) // 'z' is "greater" than 'y', etc.
                    {
                        return true;
                    } else if (first[i] < second[i])
                    {
                        return false;
                    }
                }
                return false; // "balls" is greater than "ball". if we get to the end of the loop and haven't returned, and second is longer, then it is greater
                // alternatively, if words are same length and we get to the end of the loop without returning, they are the same word, or spelled identically
            } else 
            {
                for (int i = 0; i < second.Length; i++)
                {
                    if (first[i] > second[i]) // 'z' is "greater" than 'y', etc.
                    {
                        return true;
                    } else if (first[i] < second[i])
                    {
                        return false;
                    }
                }
                return true; // "balls" is greater than "ball". if we get to the end of the loop and haven't returned, and first is longer, then it is greater
            } 
        }
    }
}
