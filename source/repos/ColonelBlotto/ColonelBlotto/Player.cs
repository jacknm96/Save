using System;
using System.Collections.Generic;
using System.Text;

namespace ColonelBlotto
{
    class Player
    {
        int numTroops;
        int maxTroops;
        int[] fronts;

        public Player(int num, int[] numFronts)
        {
            numTroops = num;
            maxTroops = num;
            fronts = numFronts;
        }

        public int[] DeployTroops(string name, string other)
        {
            Console.WriteLine(name + "'s turn. " + other + ", please look away.");
            Console.WriteLine(name + ", you have " + numTroops.ToString() + " troops. How do you want to deploy?");
            for (int i = 0; i < fronts.Length; i++)
            {
                Console.Write("Front " + (i + 1).ToString() + ": ");
                int num;
                string number = "";
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    number += key.KeyChar;
                }
                while (!int.TryParse(number, out num) || num < 0 || numTroops - num < 0)
                {
                    Console.WriteLine();
                    if (numTroops - num < 0)
                    {
                        Console.WriteLine("You do not have enough remaining troops to deploy that many.");
                        Console.Write("You have " + numTroops.ToString() + " troops remaining. How many do you wish to deploy? ");
                        num = -1;
                    } else
                    {
                        Console.Write("Please input an integer 0 or greater: ");
                    }
                    number = "";
                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                            break;
                        number += key.KeyChar;
                    }
                }
                fronts[i] = num;
                numTroops -= num;
                Console.WriteLine();
            }
            numTroops = maxTroops;
            return fronts;
        }

        public string DisplayDeployment()
        {
            string deployment = "";
            for (int i = 0; i < fronts.Length; i++)
            {
                deployment += fronts[i].ToString() + " ";
            }
            return deployment;
        }

        public void AddTroops(int captured)
        {
            numTroops += captured;
            maxTroops += captured;
        }

        public int NumTroops()
        {
            return maxTroops;
        }
    }
}
