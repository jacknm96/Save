using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Expert : Agent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            Blackboard.SetPlayerPosition(other.GetComponent<PlayerMovement>().transform);
        }
    }
}
