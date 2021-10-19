using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hazard : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] PlayerMovement2D player;
    [SerializeField] UnityEvent pickUpEvent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pickUpEvent.Invoke();
            player.Respawn();
        }
    }
}
