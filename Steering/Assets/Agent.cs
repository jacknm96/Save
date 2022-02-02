using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Agent : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float speed;
    [SerializeField] float maxSpeed;
    [SerializeField] Transform target;
    [SerializeField] bool chase;
    [SerializeField] float wanderRadius;
    [SerializeField] float jitter;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            rb.velocity = transform.forward * speed * Time.deltaTime;
            if (chase)
            {
                Seek();
            } else
            {
                Flee();
            }
        } else
        {
            Wander();
        }
    }

    void Seek()
    {
        rb.velocity += CalculateSteeringForce();
    }

    void Flee()
    {
        rb.velocity -= CalculateSteeringForce();
    }

    void Wander()
    {
        /*Vector3 target = new Vector3(transform.position.x + Random.Range(0, wanderRadius), transform.position.y, transform.position.z + Random.Range(0, wanderRadius));
        Vector3 endpoint = new Vector3(transform.position.x + Random.Range(0, wanderRadius), transform.position.y, transform.position.z + Random.Range(0, wanderRadius));
        Vector3 vector = (endpoint - target).normalized * Random.Range(1, jitter);
        rb.velocity += vector;*/
        Vector3 newTarget = Random.insideUnitSphere;
        newTarget += Random.insideUnitSphere * jitter;
        newTarget = new Vector3(newTarget.x, 0, newTarget.z);
        newTarget.Normalize();
        newTarget *= wanderRadius;
        newTarget += transform.forward * Random.Range(1, 5);
        rb.AddForce(newTarget);
    }

    Vector3 CalculateSteeringForce()
    {
        Vector3 desiredVelocity = (target.position - transform.position).normalized * maxSpeed * Time.deltaTime;
        Vector3 steeringForce = desiredVelocity - rb.velocity;
        return steeringForce;
    }
}
