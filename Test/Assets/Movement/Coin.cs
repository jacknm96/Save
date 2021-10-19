using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Collider2D player;
    [SerializeField] Display ui;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");
        if (collision = player)
        {
            Debug.Log("player found");
            gameObject.SetActive(false);
            //ui.AddCoin();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
