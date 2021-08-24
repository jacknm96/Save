using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopExample : MonoBehaviour
{
    bool[] array = {true, true, false, true, false, false, true};
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(bool i in array)
        {
            if(i)
            {
                Debug.Log("I'm Happy");
            } else
            {
                Debug.Log("I'm sad");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
