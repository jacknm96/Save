using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability", order = 1)]
public class Abilities : ScriptableObject
{
    public int ability;
    public bool isActive = false;
}
