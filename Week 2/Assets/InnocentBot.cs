using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnocentBot : MonoBehaviour
{
    public bool innocence = true;

    public InnocentBot()
    {
        Debug.Log("You made an InnocentBot");
    }

    public InnocentBot(bool guilt)
    {
        innocence = guilt;
        if (innocence)
        {
            Debug.Log("You made an InnocentBot");
        } else
        {
            Debug.Log("You made a GuiltyBot");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
