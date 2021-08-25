using System;

namespace ArrayLoopExample
{
    class Program
    {
        static string greeting = "Hello World!";
        static int[] inventory = {2, 5, 3, 1, 6, 4, 4, 22, 7, 2 };
        static string[] definitions = { "armor", "boots", "gloves", "helmets", "weapons", "shields", "potions", "ammunition", "food", "spells" };


        static void Main(string[] args)
        {
            /*float[] first = { 1f, 2f, 3f, 4f, 5f };
            int[] second = { 6, 7, 8, 9, 10 };
            float[] sum = CombineArrays(first, second);
            for (int i = 0; i < sum.Length; i++)
            {
                Console.Write(sum[i].ToString());
            }
            Console.WriteLine();
            
            for (int i = 0; i < greeting.Length; i++)
            {
                Console.WriteLine(greeting[i]);
            }
            */
            PrintInventory();
        }

        static int[] CombineArrays(int[] first, int[] second)
        {
            int[] sum = new int[first.Length + second.Length];
            for (int i = 0; i < first.Length; i++)
            {
                sum[i] = first[i];
            }
            for (int i = 0; i < second.Length; i++)
            {
                sum[first.Length + i] = second[i];
            }
            return sum;
        }

        static float[] CombineArrays(float[] first, int[] second)
        {
            float[] newSecond = new float[second.Length];
            for (int i = 0; i < second.Length; i++)
            {
                newSecond[i] = (float)second[i];
            }
            float[] sum = new float[first.Length + second.Length];
            for (int i = 0; i < first.Length; i++)
            {
                sum[i] = first[i];
            }
            for (int i = 0; i < second.Length; i++)
            {
                sum[first.Length + i] = newSecond[i];
            }
            return sum;
        }

        static void ModifyInventory(int index, int value) // index determines which item is being modified, value determines by how much
        {
            inventory[index] += value;
            if (inventory[index] < 0)
            {
                inventory[index] = 0;
            }
        }

        static void PrintInventory()
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                Console.WriteLine("I have " + inventory[i].ToString() + " " + definitions[i]);
            }
        }
    }
}
