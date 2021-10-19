using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyGroundChecker checker;
    Rigidbody2D rb;
    Animator anim;
    public SpriteRenderer rend;
    [SerializeField] float speed = 3f;
    [SerializeField] PlayerMovement2D player;
    [SerializeField] UnityEvent pickUpEvent;
    Vector3 startPosition;
    bool startFace;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        startFace = rend.flipX;
        Display.enemies.Add(this);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (rend.flipX)
        {
            rb.velocity = Vector2.left * speed;
        } else
        {
            rb.velocity = Vector2.right * speed;
        }
    }

    public void Restart()
    {
        transform.position = startPosition;
        rend.flipX = startFace;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            Debug.Log(collision.gameObject.name);
            pickUpEvent.Invoke();
            player.Respawn();
        }
    }
}
