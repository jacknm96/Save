using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Player Data", menuName = "Create Player Data")]
public class PlayerData : ScriptableObject
{
    [SerializeField] private int[] startInventory = new int[6];
    public int[] inventory = new int[6];

    public void StartInventory()
    {
        for (int i = 0; i < startInventory.Length; i++)
        {
            inventory[i] = startInventory[i];
        }
    }

    public void UseItem(int i)
    {
        inventory[i]--;
    }
}
