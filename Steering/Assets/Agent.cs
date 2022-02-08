using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Agent : MonoBehaviour
{
    public enum Behavior
    {
        seek, wander, flee, pursue, evade
    }
    
    Rigidbody rb;

    [SerializeField] float maxSpeed;
    [SerializeField] Transform target;
    [SerializeField] Rigidbody targetRb;
    [SerializeField] float wanderRadius;
    [SerializeField] float jitter;

    public Behavior behavior;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            ApplyBehavior();
        } else
        {
            Wander();
        }
    }

    void ApplyBehavior()
    {
        switch (behavior)
        {
            case Behavior.seek:
                Seek();
                break;
            case Behavior.flee:
                Flee();
                break;
            case Behavior.wander:
                Wander();
                break;
            case Behavior.pursue:
                Pursue();
                break;
            case Behavior.evade:
                Evade();
                break;
            default:
                break;
        }
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
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

    void Pursue()
    {
        rb.AddForce(CalculatePursueForce().normalized * maxSpeed - rb.velocity);
    }

    void Evade()
    {
        rb.AddForce(Vector3.Cross(CalculatePursueForce(), transform.up));
    }

    void Dodge(GameObject obstacle)
    {
        Vector3 V = CalculatePursueForce(obstacle);
        Vector3 D = Quaternion.Euler(new Vector3(0, 90, 0)) * rb.velocity;
        rb.AddForce(D * maxSpeed - rb.velocity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Dodge(other.gameObject);
        }
    }

    Vector3 CalculatePursueForce()
    {
        return targetRb.velocity + target.position - transform.position;
    }

    Vector3 CalculatePursueForce(GameObject obstacle)
    {
        return obstacle.GetComponent<Rigidbody>().velocity + obstacle.transform.position - transform.position;
    }

    Vector3 CalculateSteeringForce()
    {
        Vector3 desiredVelocity = (target.position - transform.position).normalized * maxSpeed * Time.deltaTime;
        Vector3 steeringForce = desiredVelocity - rb.velocity;
        return steeringForce;
    }

    void ChangeTarget(Agent other)
    {
        target = other.transform;
        targetRb = other.GetComponent<Rigidbody>();
    }
}
