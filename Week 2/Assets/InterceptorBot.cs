using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorBot : MonoBehaviour
{
    public float speed = 40f;

    // constructor
    public InterceptorBot()
    {
        Debug.Log("You made a default Interceptor");
    }

    public InterceptorBot(float setSpeed)
    {
        speed = setSpeed;
        Debug.Log("You made a custom Interceptor");
    }

    private void Awake()
    {
        speed = Random.Range(10, 100);
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
