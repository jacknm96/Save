using System;
using System.Collections.Generic;

namespace StoreTransaction
{
    public static class Transaction
    {
        static List<string> storeInventory = new List<string>();
        static List<string> playerInventory = new List<string>();
        static float playerMoney;
        
        public static void Payment(string item, float amount)
        {
            if (playerMoney > amount)
            {
                playerInventory.Add(item);
                storeInventory.Remove(item);
                playerMoney -= amount;
            }
        }

        public static void Trade(string item1, string item2, List<string> inventory1, List<string> inventory2)
        {
            if (CheckInventory(item1, inventory1) && CheckInventory(item2, inventory2))
            {
                inventory1.Add(item2);
                inventory1.Remove(item1);
                inventory2.Add(item1);
                inventory2.Remove(item2);
            }
        }

        public static float Rob(List<float> tills)
        {
            float amount = 0;
            for (int i = 0; i < tills.Count; i++)
            {
                amount += tills[i];
                tills[i] = 0;
            }
            return amount;
        }

        public static void ReturnItem(string item, List<string> inventory1, List<string> inventory2)
        {
            if (CheckInventory(item, inventory1))
            {
                inventory1.Remove(item);
                inventory2.Add(item);
            }
        }

        public static bool CheckInventory(string item, List<string> inventory)
        {
            foreach (string b in inventory)
            {
                if (b == item)
                {
                    return true;
                }
            }
            return false;
        }

        public static List<List<string>> BagGroceries(List<string> groceries)
        {
            List<List<string>> bags = new List<List<string>>();
            int num = 0;
            int bag = 0;
            foreach (string item in groceries)
            {
                if (num == 5)
                {
                    num = 0;
                    bag++;
                }
                bags[bag].Add(item);
                num++;
            }
            return bags;
        }
    }
}
