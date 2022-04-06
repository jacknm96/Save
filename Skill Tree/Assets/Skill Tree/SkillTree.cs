using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    [SerializeField] Node root;

    // Start is called before the first frame update
    void Start()
    {
        PrimeTree(root);
        root.MakeAvailable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrimeTree(Node curr)
    {
        if (!curr.childrenPrimed)
        {
            foreach (Node child in curr.GetChildren())
            {
                child.PrimeParent();
                PrimeTree(child);
            }
            curr.childrenPrimed = true;
        }
    }
}
