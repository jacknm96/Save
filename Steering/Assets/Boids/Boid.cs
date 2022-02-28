using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(BoxCollider))]
public class Boid : MonoBehaviour
{
    float neighborhoodRadius = 8;

    [SerializeField] List<Boid> neighbors;

    [SerializeField] Boid leader;

    [Range(0.1f, 1f)]
    [SerializeField] float separationWeight = 1;
    [Range(0.1f, 1f)]
    [SerializeField] float alignmentWeight = 1;
    [Range(0.1f, 1f)]
    [SerializeField] float cohesionWeight = 1;

    [SerializeField] float speed;
    [SerializeField] float maxForce;

    public Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<SphereCollider>().isTrigger = true;
        GetComponent<SphereCollider>().radius = neighborhoodRadius / 2;
        neighbors = new List<Boid>();
        neighbors.Add(leader);
        //rb.velocity = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1)) * Random.Range(1, speed);
    }

    private void FixedUpdate()
    {
        if (neighbors.Count > 0)
        {
            WeightedTruncatedRunningSum();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Boid>() != null)
        {
            if (!neighbors.Contains(other.GetComponent<Boid>()))
            {
                neighbors.Add(other.GetComponent<Boid>());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Boid>() != null && other.GetComponent<Boid>() != leader)
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

    Vector3[] GetVectors() // slot 0 is separation, 1 is alignment, 2 is cohesion
    {
        Vector3[] vectors = new Vector3[3];
        vectors[0] = Vector3.zero;
        vectors[1] = Vector3.zero;
        vectors[2] = Vector3.zero;
        foreach (Boid boid in neighbors)
        {
            vectors[0] += -(boid.transform.position - transform.position);
            vectors[1] += boid.GetVelocity();
            vectors[2] += boid.transform.position - transform.position;
        }
        vectors[0] /= neighbors.Count;
        vectors[1] /= neighbors.Count;
        vectors[2] /= neighbors.Count;
        return vectors;
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
        Vector3[] vectors = GetVectors();
        rb.AddForce((vectors[0] + vectors[1] + vectors[2]) * speed);
    }

    void WeightedSum()
    {
        Vector3[] vectors = GetVectors();
        rb.AddForce((vectors[0] * separationWeight + vectors[1] * alignmentWeight + vectors[2] * cohesionWeight) * speed);
    }

    void WeightedTruncatedRunningSum()
    {
        Vector3[] vectors = GetVectors();
        float angle = Vector3.Angle(vectors[1], rb.velocity) / 180;
        float averageDistance = 0;
        if (neighbors.Count > 0)
        {
            foreach (Boid boid in neighbors)
            {
                averageDistance += Vector3.Distance(boid.transform.position, transform.position);
            }
            averageDistance /= neighbors.Count;
        }
        averageDistance /= neighborhoodRadius;
        List<float> values = new List<float>();
        values.Add(angle);
        values.Add(averageDistance);
        values.Add(1 - averageDistance);
        values.Sort();
        Vector3 forceToApply = Vector3.zero;
        float forceApplied = 0;
        for (int i = 2; i >= 0; i--)
        {
            if (values[i] == angle)
            {
                if (forceApplied < maxForce)
                {
                    float addTo = (vectors[1] * alignmentWeight * speed).magnitude;
                    if (forceApplied + addTo > maxForce)
                    {
                        addTo = maxForce - forceApplied;
                    }
                    forceToApply += (vectors[1] * alignmentWeight * speed).normalized * addTo;
                    forceApplied += addTo;
                }
            } else if (values[i] == averageDistance)
            {
                if (forceApplied < maxForce)
                {
                    float addTo = (vectors[2] * cohesionWeight * speed).magnitude;
                    if (forceApplied + addTo > maxForce)
                    {
                        addTo = maxForce - forceApplied;
                    }
                    forceToApply += (vectors[2] * cohesionWeight * speed).normalized * addTo;
                    forceApplied += addTo;
                }
            } else
            {
                if (forceApplied < maxForce)
                {
                    float addTo = (vectors[0] * separationWeight * speed).magnitude;
                    if (forceApplied + addTo > maxForce)
                    {
                        addTo = maxForce - forceApplied;
                    }
                    forceToApply += (vectors[0] * separationWeight * speed).normalized * addTo;
                    forceApplied += addTo;
                }
            }
        }
        if (leader == this)
        {
            forceToApply /= 2;
        }
        forceToApply += transform.forward * 2;
        rb.AddForce(forceToApply * speed);
    }
}
