using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "newCharacter", menuName = "GameData/Character")]

public class CharacterStats : ScriptableObject
{
    public string characerName;
    public int[] inventory;
    public float maxHealth;
    public Image image;

    [Header("Starting Settings")]
    [SerializeField] float startingHealth;

    public void Awake()
    {
        startingHealth = maxHealth;
    }
}
