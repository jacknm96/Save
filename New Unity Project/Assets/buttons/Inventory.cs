using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    /*
    public Dictionary<string, float> itemCosts = new Dictionary<string, float>();
    public List<string> items = new List<string>();
    public List<float> costs = new List<float>();

    public void AddItemsToDictionary()
    {
        for (int i = 0; i < items.Count; i++)
        {
            itemCosts.Add(items[i], costs[i]);
        }
    }

    public void RemoveItemsFromDictionary()
    {
        for (int i = 0; i < items.Count; i++)
        {
            itemCosts.Remove(items[i]);
        }
    }
    */

    [SerializeField] ItemRepresentation[] inventoryDisplay;

    public PlayerData playerData;

    private void Start()
    {
        playerData.StartInventory();
        UpdateInventoryDisplay();
    }


    void UpdateInventoryDisplay()
    {
        for (int i = 0; i < inventoryDisplay.Length; i++)
        {
            inventoryDisplay[i].UpdateButton(playerData.inventory[i]);
        }
    }
}
