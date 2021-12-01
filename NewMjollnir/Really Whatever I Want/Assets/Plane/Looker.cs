using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looker : MonoBehaviour
{
    Vector2 direction;
    Rigidbody rb;
    [SerializeField] float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -295 && direction.y > 0)
        {
            direction.y = 0;
        }
        if (transform.position.y > -305 && direction.y < 0)
        {
            direction.y = 0;
        }
        if (transform.position.x > -405 && direction.x < 0)
        {
            direction.x = 0;
        }
        if (transform.position.x < -395 && direction.x > 0)
        {
            direction.x = 0;
        }
        rb.velocity = direction * speed;
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }
}
