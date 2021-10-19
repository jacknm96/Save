using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Collectables : MonoBehaviour
{
    public static int maxCollectable;
    public static int collectablesCollected;
    [SerializeField] UnityEvent pickUpEvent;
    Rigidbody2D rb;
    Vector3 startPosition;
    [SerializeField] PlayerMovement2D player;

    // Start is called before the first frame update
    void Start()
    {
        maxCollectable++;
        Display.coins.Add(this);
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Float());
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collected();
        }
    }

    void Collected()
    {
        collectablesCollected++;
        pickUpEvent.Invoke();
        Display.UpdateDisplay();
        CheckAllCollected();
        gameObject.SetActive(false);
    }

    public void ReActivate()
    {
        StopAllCoroutines();
        gameObject.SetActive(true);
        transform.position = startPosition;
        rb.velocity = Vector3.zero;
        StartCoroutine(Float());
    }

    void CheckAllCollected()
    {
        if (collectablesCollected == maxCollectable)
        {
            player.Winning();
            Display.VictoryMessage();
        }
    }

    IEnumerator Float()
    {
        while (true)
        {
            rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
            yield return new WaitForSeconds(.5f);
            rb.AddForce(new Vector2(0, -2), ForceMode2D.Impulse);
            yield return new WaitForSeconds(.5f);
            rb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        }
    }
}
