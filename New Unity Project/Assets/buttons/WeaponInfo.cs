using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct weaponInfo
{
    public string name;
    public WeaponType type;
    public float cost;
    public float damage;
    public float durability;
    public bool cursed;
}

public enum WeaponType
{
    onehanded, twohanded, ranged, magic
}