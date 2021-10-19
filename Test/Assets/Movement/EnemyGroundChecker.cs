using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundChecker : MonoBehaviour
{
    public LayerMask groundLayer;
    [SerializeField] Collider2D enemyCollider;
    [SerializeField] Enemy enemy;

    private void Start()
    {
        Physics2D.IgnoreCollision(enemyCollider, GetComponent<Collider2D>());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            enemy.rend.flipX = !enemy.rend.flipX;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EditorOnly"))
        {
            enemy.rend.flipX = !enemy.rend.flipX;
        }
    }
}
