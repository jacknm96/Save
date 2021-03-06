using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Fuel : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    Player player;

    private void Awake()
    {
        player = Player.instance;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -speed);
    }

    private void FixedUpdate()
    {
        if (transform.position.z < -5)
        {
            Delete();
        }
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public void Delete()
    {
        ObjectPool.Recycle(this);
    }

    public Fuel(Vector2 position)
    {
        rb = GetComponent<Rigidbody>();
        transform.position.Set(position.x, position.y, 0);
        rb.velocity = new Vector3(0, 0, -speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        Delete();
    }
}
