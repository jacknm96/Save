using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] bool scorer;
    Player player;
    float xRotate;
    float yRotate;
    float zRotate;
    bool rotate;
    Vector3 center;

    public Obstacle(Vector2 position)
    {
        rb = GetComponent<Rigidbody>();
        transform.position.Set(position.x, position.y, 0);
        rb.velocity = new Vector3(0, 0, -speed);
    }

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
        rotate = true;
    }

    private void FixedUpdate()
    {
        if (transform.position.z < -5)
        {
            Delete();
        }
        if (rotate)
        {
            center = gameObject.GetComponent<Renderer>().bounds.center;
            transform.Rotate(0, yRotate, 0);
            transform.RotateAround(center, Vector3.right, -xRotate);
            transform.RotateAround(center, Vector3.forward, zRotate);
        }
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
        rotate = false;
    }

    public void Delete()
    {
        ObjectPool.Recycle(this);
        if (scorer)
        {
            player.AdjustScore();
        }
    } 
}
