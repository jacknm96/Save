using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 200;
    Vector2 direction;
    float jumpForce = 8f;
    [SerializeField] GroundCheck checker;
    Animator anim;
    SpriteRenderer rend;
    [SerializeField] SpawnZone spawn;
    bool won;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        anim.SetFloat("speed", Mathf.Abs(direction.x));
        if (direction.x < 0)
        {
            rend.flipX = true;
        } else
        {
            rend.flipX = false;
        }
    }

    public void Winning()
    {
        won = true;
    }

    private void FixedUpdate()
    {
        Move();
        anim.SetBool("onground", checker.onGround);
    }

    private void Move()
    {
        float vertical = rb.velocity.y;
        float horizontal = direction.x * speed;
        rb.velocity = new Vector2(horizontal, vertical); 
    }

    private void Jump()
    {
        if (checker.onGround)
        {
            anim.SetTrigger("jumping");
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }

    [ContextMenu("Respawn")]
    public void Respawn()
    {
        if (!won) 
        {
            transform.position = spawn.transform.position;
            Display.Restart();
        }
    }
}
