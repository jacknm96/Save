using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool onGround;
    public LayerMask groundLayer;
    [SerializeField] Collider2D player;

    private void Start()
    {
        Physics2D.IgnoreCollision(player, GetComponent<Collider2D>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            onGround = false;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        onGround = false;
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Finish"))
        {
            onGround = true;
        }
    }
}
