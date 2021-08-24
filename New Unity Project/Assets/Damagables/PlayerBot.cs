using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 

public class PlayerBot : DamagableBody
{
    float horizontal;
    float vertical;
    Rigidbody rb;
    [SerializeField] float speed = 2f;
    [SerializeField] float rotationalSpeed = 5f;

    private void Start()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            rb = GetComponent<Rigidbody>();
        }
        else
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        CheckForDeath();
    }

    public override void CheckForDeath()
    {
        base.CheckForDeath();
    }

    public override void Move()
    {
        rb.velocity += transform.forward * horizontal * speed * Time.fixedDeltaTime;
        transform.Rotate(0, vertical * rotationalSpeed * Time.fixedDeltaTime, 0);
    }

    public override void OnDeath()
    {
        health = 0;
        isAlive = false;
    }
}
