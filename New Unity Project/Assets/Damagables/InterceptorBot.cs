using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorBot : DamagableBody
{
    public float speed = 40f;
    public PlayerBot player;

    private void FixedUpdate()
    {
        Move();
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        health += Random.Range(-2, 5);
        CheckForDeath();
    }

    public override void CheckForDeath()
    {
        base.CheckForDeath();
    }

    public override void Move()
    {
        // transform.position += (player.transform.position - transform.position).normalized * speed * Time.fixedDeltaTime;
    }

    public override void OnDeath()
    {
        health = 0;
        isAlive = false;
    }
}
