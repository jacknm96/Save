using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WeaponTypeExtension
{
    public static WeaponType GetRandomType (this WeaponType type)
    {
        int i = Random.Range(0, 4);
        switch(i)
        {
            case 0:
                type = WeaponType.magic;
                return WeaponType.magic;
            case 1:
                type = WeaponType.onehanded;
                return WeaponType.onehanded;
            case 2:
                type = WeaponType.twohanded;
                return WeaponType.twohanded;
            case 3:
                type = WeaponType.ranged;
                return WeaponType.ranged;
            default:
                type = WeaponType.magic;
                return WeaponType.magic;
        }
    }
}
