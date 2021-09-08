using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CityMovement : MonoBehaviour
{
    [HideInInspector] public Rigidbody rb;
    [SerializeField] protected float speed;
    [SerializeField] protected float sprint;
    [SerializeField] protected float rotation;
    [SerializeField] protected bool canMove;

    // Start is called before the first frame update
    virtual public void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (canMove)
        {
            Movement();
        }
    }

    abstract public void Movement();
}
