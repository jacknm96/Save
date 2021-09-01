using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public Dictionary<int, weaponInfo> weapons = new Dictionary<int, weaponInfo>();
    public List<int> items = new List<int>();
    public List<weaponInfo> data = new List<weaponInfo>();

    weaponInfo CreateWeaponInfo()
    {
        weaponInfo WI;
        WI.name = "steve";
        WI.type = WeaponType.onehanded;
        WI.type.GetRandomType();
        WI.cost = (Random.Range(0, 2) < 1) ? 3f : 2f;
        WI.damage = Random.Range(0f, 2f);
        WI.durability = Random.Range(0, 100f);
        WI.cursed = Random.Range(0, 2) < 1;
        return WI;
    }

    void Start()
    {
        for (int i = 0; i < items.Count; i++)
        {
            data.Add(CreateWeaponInfo());
        }

        AddItemsToDictionary();
    }

    public void AddItemsToDictionary()
    {
        for (int i = 0; i < items.Count; i++)
        {
            weapons.Add(items[i], data[i]);
        }
    }

    public void RemoveItemsFromDictionary()
    {
        weapons.Clear();
    }
    
    // Start is called before the first frame update
    
}
