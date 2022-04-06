using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        transform.forward = homeTurret.transform.forward;
        rb.velocity = transform.forward * speed;
    }

    protected override void Hit(GameObject o)
    {
        homeTurret.RecycleBullet(this);
    }
}
