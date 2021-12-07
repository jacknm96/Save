using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Looker : MonoBehaviour
{
    Vector2 direction;
    Rigidbody rb;
    Vector3 startPos;
    [SerializeField] float speed;

    [SerializeField] Player player;

    [SerializeField] WallCollider topCollider;
    [SerializeField] WallCollider leftCollider;
    [SerializeField] WallCollider rightCollider;
    [SerializeField] WallCollider bottomCollider;

    // Start is called before the first frame update
    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.lost)
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
            rb.velocity = direction * speed;
            if (direction.y == 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, player.transform.position.y - transform.position.y, 0);
            }
            if (direction.x == 0)
            {
                rb.velocity = new Vector3(player.transform.position.x - transform.position.x, rb.velocity.y,  0);
            }
            if (player.isBoosting) rb.velocity *= 2;
        }
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    public void Restart()
    {
        transform.localPosition = startPos;
        topCollider.hitWall = false;
        bottomCollider.hitWall = false;
        leftCollider.hitWall = false;
        rightCollider.hitWall = false;
    }
}
