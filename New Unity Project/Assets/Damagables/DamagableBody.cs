using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamagableBody : MonoBehaviour, IDamagable
{
    public int health = 100;
    public bool hasGroceries = false;
    public bool isAlive = true;

    public virtual void TakeDamage(int dmg = 10)
    {
        health -= dmg;
    }

    public abstract void OnDeath();

    public abstract void Move();

    public virtual void CheckForDeath()
    {
        if (health <= 0)
        {
            OnDeath();
        }
    }
}
