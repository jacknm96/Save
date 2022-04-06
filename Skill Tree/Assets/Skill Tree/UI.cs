using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] TMP_Text experienceText;

    [SerializeField] int experience;
    
    // Start is called before the first frame update
    void Start()
    {
        experienceText.text = experience.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanAfford(int cost)
    {
        return experience - cost >= 0;
    }

    public void DeductCost(int cost)
    {
        experience -= cost;
        experienceText.text = experience.ToString();
    }
}
