using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemData", menuName = "Create ItemData")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int index;
}
