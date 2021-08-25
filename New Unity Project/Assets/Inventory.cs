using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [System.Serializable]

    class ItemRepresentation
    {
        public TMP_Text itemCount;
        public TMP_Text itemName;
        public Image icon;
    }

    [SerializeField] ItemRepresentation[] inventoryDisplay;

    #region arrayValues
    string[] itemNames = { "Potions", "Mana", "Oak Nutts", "Socks", "Shields", "Hats" };
    public int[] inventory = new int[6];
    public Sprite[] spriteIcons = new Sprite[6];
    #endregion
    void UpdateInventoryDisplay()
    {
        for (int i = 0; i < inventoryDisplay.Length; i++)
        {
            inventoryDisplay[i].itemName.text = itemNames[i];
            inventoryDisplay[i].itemCount.text = inventory[i].ToString();
            inventoryDisplay[i].icon.sprite = spriteIcons[i];
        }
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        UpdateInventoryDisplay();
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
