using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCosts : MonoBehaviour
{
    public static Dictionary<string, float> costs = new Dictionary<string, float>{ { "potions", 5f }, { "mana", 7f }, { "oak nuts", 1f }, { "socks", 3f }, { "shields", 15f }, { "hats", 6f } };
}
