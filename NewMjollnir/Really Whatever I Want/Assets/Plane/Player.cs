using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    Rigidbody rb;
    Vector2 direction;
    Quaternion baseRotation;
    [SerializeField] InputActionReference move;
    [SerializeField] float speed;

    [SerializeField] WallCollider topCollider;
    [SerializeField] WallCollider leftCollider;
    [SerializeField] WallCollider rightCollider;
    [SerializeField] WallCollider bottomCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        baseRotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        if (direction.magnitude > 0.1f)
        {
            rb.velocity = direction * speed;
        } else
        {
            rb.velocity = Vector2.zero;
            transform.rotation = baseRotation;
        }
    }

    private void FixedUpdate()
    {
        if (topCollider.hitWall && direction.y > 0)
        {
            direction.y = 0;
        }
        if (bottomCollider.hitWall && direction.y < 0)
        {
            direction.y = 0;
        }
        if (leftCollider.hitWall && direction.x < 0)
        {
            direction.x = 0;
        }
        if (rightCollider.hitWall && direction.x > 0)
        {
            direction.x = 0;
        }
    }

    private void OnMove(InputValue value)
    {
        transform.rotation = baseRotation;
        direction = value.Get<Vector2>();
        transform.Rotate(new Vector3(-direction.y * 30, direction.x * 30, 0));
    }
}
