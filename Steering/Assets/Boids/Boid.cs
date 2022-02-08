using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Boid : MonoBehaviour
{
    float neighborhoodRadius = 4;

    List<Boid> neighbors;

    [Range(0.1f, 1f)]
    [SerializeField] float separationWeight = 1;
    [Range(0.1f, 1f)]
    [SerializeField] float alignmentWeight = 1;
    [Range(0.1f, 1f)]
    [SerializeField] float cohesionWeight = 1;

    [SerializeField] float speed;

    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<SphereCollider>().radius = neighborhoodRadius;
        neighbors = new List<Boid>();
    }

    private void FixedUpdate()
    {
        if (neighbors.Count > 0)
        {
            WeightedSum();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Boid>() != null)
        {
            neighbors.Add(other.GetComponent<Boid>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Boid>() != null)
        {
            neighbors.Remove(other.GetComponent<Boid>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetVelocity()
    {
        return rb.velocity;
    }

    Vector3 Separation() // avoid neighbors
    {
        Vector3 separation = Vector3.zero;
        foreach(Boid boid in neighbors)
        {
            separation += -(boid.transform.position - transform.position);
        }
        return separation / neighbors.Count;
    }

    Vector3 Alignment() // orient to neighbors' average velocity
    {
        Vector3 alignment = Vector3.zero;
        foreach (Boid boid in neighbors)
        {
            alignment += boid.GetVelocity();
        }
        return alignment / neighbors.Count;
    }

    Vector3 Cohesion() // move towards average position
    {
        Vector3 cohesion = Vector3.zero;
        foreach (Boid boid in neighbors)
        {
            cohesion += boid.transform.position - transform.position;
        }
        return cohesion / neighbors.Count;
    }

    void SimpleSum()
    {
        rb.AddForce((Separation() + Alignment() + Cohesion()) * speed);
    }

    void WeightedSum()
    {
        rb.AddForce((Separation() * separationWeight + Alignment() * alignmentWeight + Cohesion() * cohesionWeight) * speed);
    }
}
