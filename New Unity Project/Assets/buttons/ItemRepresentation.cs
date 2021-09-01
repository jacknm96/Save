using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemRepresentation : MonoBehaviour
{
    public TMP_Text itemCount;
    public TMP_Text itemNameDisplay;
    public Image icon;
    public ItemData data;
    public PlayerData player;

    public void UpdateButton(int amount)
    {
        itemNameDisplay.text = data.itemName;
        itemCount.text = amount.ToString();
        icon.sprite = data.sprite;
    } 

    public void UseItem(int i)
    {
        if (player.inventory[i] > 0)
        {
            player.UseItem(i);
            UpdateButton(player.inventory[i]);
        }
    }
}
