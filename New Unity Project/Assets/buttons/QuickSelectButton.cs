using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuickSelectButton : MonoBehaviour
{
    public TMP_Text itemCount;
    public TMP_Text itemNameDisplay;
    public Image icon;
    ItemData currentData;
    public TMP_Text newItem;
    [SerializeField] QuickSelectInventory inventorySelector;

    public void  InitializeButton(ItemData data)
    {
        currentData = data;
        itemNameDisplay.text = data.itemName;
        icon.sprite = data.sprite;
        itemCount.text = QuickSelectInventory.possibleInventory[data.index].ToString();
    }

    void UpdateButton()
    {
        itemCount.text = QuickSelectInventory.possibleInventory[currentData.index].ToString();
    }

    public void ChangeItem(int i)
    {
        InitializeButton(inventorySelector.items[i]);
    }

    public void ChangeItem(string s)
    {
        InitializeButton(inventorySelector.items[int.Parse(s)]);
    }

    public void UseItem()
    {
        QuickSelectInventory.possibleInventory[currentData.index]--;
        UpdateButton();
    }
}
