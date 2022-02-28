using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class NPC : MonoBehaviour
{
    bool playerNearby;
    bool started;
    Rigidbody rb;

    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform taskDestination;
    [SerializeField] Transform endDestination;
    [SerializeField] Player player;
    [SerializeField] Material skin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        skin.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            playerNearby = true;
            if (!started)
            {
                started = true;
                StartCoroutine(GoToTaskDestination());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            playerNearby = false;
        }
    }

    IEnumerator GoToTaskDestination()
    {
        Vector3 dest = taskDestination.position;
        dest = new Vector3(dest.x, transform.position.y, dest.z);
        agent.SetDestination(dest);
        while ((dest - transform.position).magnitude > 10)
        {
            if (playerNearby)
            {
                agent.speed = 3.5f;
            } else
            {
                agent.speed = 0;
            }
            yield return null;
        }
        StartCoroutine(PerformTask());
    }

    IEnumerator PerformTask()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(10);
        skin.color = Color.blue;
        StartCoroutine(FollowToEndDestination());
    }

    IEnumerator FollowToEndDestination()
    {
        Vector3 dest = endDestination.position;
        while ((dest - transform.position).magnitude > 10)
        {
            agent.SetDestination(player.transform.position);
            if (playerNearby)
            {
                agent.speed = 3.5f;
            }
            else
            {
                agent.speed = 7.5f;
            }
            yield return null;
        }
        agent.SetDestination(transform.position);
        rb.velocity = Vector3.zero;
        skin.color = Color.green;
    }
}
