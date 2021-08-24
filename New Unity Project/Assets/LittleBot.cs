using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBot : MonoBehaviour
{
    public GameObject alarm;
    static List<LittleBot> bots = new List<LittleBot>();
    Rigidbody rb;
    [SerializeField] float speed = 35;
    static GameObject player;
    GameObject Player
    {
        get
        {
            return player;
        }
        set
        {
            player = value;
            SetAlarms(true);
        }
    }

    
    // Start is called before the first frame update
    public void Start()
    {
        if (!bots.Contains(this))
        {
            bots.Add(this);
        }
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            rb.velocity = DirectionToPlayer() * speed;
        }
    }

    static void SetAlarms(bool val)
    {
        foreach (LittleBot b in bots)
        {
            b.alarm.SetActive(val);
            // b.rb.velocity = b.DirectionToPlayer() * b.speed;
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("We have spotted the player");
            Player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("We have lost the player");
            Player = null;
            SetAlarms(false);
        }
    }

    Vector3 DirectionToPlayer()
    {
        if (player != null)
        {
            return (player.transform.position - transform.position).normalized;
        }
        return Vector3.zero;
    }
}
