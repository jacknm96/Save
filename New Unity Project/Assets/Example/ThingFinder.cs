using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThingFinder : MonoBehaviour
{
    [SerializeField] Thing[] things;
    
    // Start is called before the first frame update
    void Start()
    {
        things = FindObjectsOfType<Thing>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
