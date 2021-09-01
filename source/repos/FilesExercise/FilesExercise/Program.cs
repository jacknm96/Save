using System;
using System.IO;
using System.Collections.Generic;

namespace FilesExercise
{
    class Program
    {
        public static Dictionary<string, int> defs = new Dictionary<string, int>();
        public static string[] letters = new string[26] {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j",
            "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};

        static void Main(string[] args)
        {
            for (int i = 0; i < letters.Length; i++)
            {
                defs.Add(letters[i], i);
            }
             ReadFile("words");
        }

        static bool Test()
        {
            return 'a' > 'b';
        }

        static void ReadFile(string name)
        {
            StreamReader reader = new StreamReader(name + ".txt");
            int size = int.Parse(reader.ReadLine());
            string[] words = new string[size];
            for (int i = 0; i < size; i++)
            {
                words[i] = reader.ReadLine();
            }
            reader.Close();
            CocktailShakerSort(words);
            StreamWriter writer = new StreamWriter("output.txt");
            foreach (string word in words)
            {
                writer.WriteLine(word);
            }
            writer.Close();
        }

        static void CocktailShakerSort(string[] array)
        {
            if (array.Length < 2)
            {
                return;
            }
            bool sorted = false;
            int j = 1;
            while (!sorted)
            {
                sorted = true;
                int switchIndex = 0;
                for (int i = 0; i < array.Length - j; i++)
                {
                    if (FirstGreater(array[i + 1], array[i]))
                    {
                        switchIndex = i;
                        string temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        sorted = false;
                    }
                }
                if (sorted)
                {
                    return;
                }
                for (int i = switchIndex; i > j; i--)
                {
                     if (FirstGreater(array[i], array[i - 1]))
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

        static bool FirstGreater(string a, string b)
        {
            if (a.Length < b.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] > b[i])
                    {
                        return true;
                    }
                }
            } else
            {
                for (int i = 0; i < b.Length; i++)
                {
                    if (a[i] > b[i])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
