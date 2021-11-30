using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{
    public bool hitWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EditorOnly"))
        {
            hitWall = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EditorOnly"))
        {
            hitWall = false;
        }
    }
}
