using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    Player player;
    float xRotate;
    float yRotate;
    float zRotate;

    private void Start()
    {
        player = Player.instance;
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, -speed);
        xRotate = Random.Range(1, 5);
        yRotate = Random.Range(1, 5);
        zRotate = Random.Range(1, 5);
    }

    private void FixedUpdate()
    {
        if (transform.position.z < -5)
        {
            Delete();
        }
        transform.Rotate(0, yRotate, 0);
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    public void Delete()
    {
        ObjectPool.Recycle(this);
        player.AdjustScore();
    }

    public Obstacle(Vector2 position)
    {
        rb = GetComponent<Rigidbody>();
        transform.position.Set(position.x, position.y, 0);
        rb.velocity = new Vector3(0, 0, -speed);
    }
}
