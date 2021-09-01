using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuickSelectInventory : MonoBehaviour
{
    public static int[] possibleInventory = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //0 - Health potions
    //1 - mana potions
    //2 - oak nuts
    //3 - socks
    //4 - shields
    //5 - hats
    //6 - bows
    //7 - swords
    //8 - helmets
    //9 - plate armor
    //10 - leather armor
    //11 - shirts

    public ItemData[] items;
    [SerializeField] QuickSelectButton[] buttons;

    public static int[] quickSelectDisplay = new int[6];

    
    [ContextMenu("Save")]
    void SaveInventory()
    {
        StreamWriter writer = new StreamWriter("text.txt");
        for (int i = 0; i < possibleInventory.Length - 1; i++)
        {
            writer.Write(possibleInventory[i].ToString() + " ");
        }
        writer.Write(possibleInventory[possibleInventory.Length - 1].ToString());
        writer.WriteLine();
        for (int i = 0; i < quickSelectDisplay.Length - 1; i++)
        {
            writer.Write(quickSelectDisplay[i].ToString() + " ");
        }
        writer.Write(quickSelectDisplay[quickSelectDisplay.Length - 1].ToString());
        writer.WriteLine();
        writer.Close();
    }


    [ContextMenu("Load")]
    void LoadInventory()
    {
        StreamReader reader = new StreamReader("text.txt");
        string[] arrays = new string[2];
        for (int i = 0; i < arrays.Length; i++)
        {
            arrays[i] = reader.ReadLine();
        }
        string[] possible = arrays[0].Split(' ');
        string[] quick = arrays[1].Split(' ');
        Debug.Log(arrays[0]);
        Debug.Log(arrays[1]);
        for (int i = 0; i < possible.Length; i++)
        {
            possibleInventory[i] = int.Parse(possible[i]);
        }
        for (int i = 0; i < quick.Length; i++)
        {
            quickSelectDisplay[i] = int.Parse(quick[i]);
        }
        reader.Close();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        LoadInventory();
        SetUpButtons();
    }

    void SetUpButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].InitializeButton(items[quickSelectDisplay[i]]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
