using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : Bullet
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
        transform.rotation = Quaternion.LookRotation(homeTurret.transform.forward);
        rb.velocity = transform.forward * speed;
    }

    protected override void Hit(GameObject o)
    {
        
    }
}
