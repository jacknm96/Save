using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] NavMeshAgent agent;
    [SerializeField] NPC npc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(npc.transform.position);
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Die();
        } else if (other.GetComponent<NPC>() && (other.transform.position - transform.position).magnitude < 2)
        {
            other.GetComponent<NPC>().Die();
        }
    }
}
