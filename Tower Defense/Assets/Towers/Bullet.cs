using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Bullet : MonoBehaviour
{
    protected Tower homeTurret;
    protected Rigidbody rb;
    [SerializeField] protected float damageAmount;
    [SerializeField] protected float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHome(Tower home)
    {
        homeTurret = home;
    }

    public float GetDamage()
    {
        return damageAmount;
    }

    protected abstract void Hit(GameObject o);

    protected void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Tower>())
        {
            Hit(other.gameObject);
        }
    }
}
