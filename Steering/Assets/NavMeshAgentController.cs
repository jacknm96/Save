using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentController : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    RaycastHit hit;
    Ray ray;
    int num;
    int position;
    
    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgentController[] all = FindObjectsOfType<NavMeshAgentController>();
        num = all.Length;
        for (int i = 0; i < num; i++)
        {
            if (all[i] == this)
            {
                position = i;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            HollowSquareFormation(hit.point, 3f);
        }
    }

    void SquareFormation(Vector3 corner, float space)
    {
        int dimensions = (int)Mathf.Sqrt(num);
        Vector3 destination = corner + new Vector3((position % dimensions) * space, 0, (position / dimensions) * space);
        agent.SetDestination(destination);
    }

    void HollowSquareFormation(Vector3 corner, float space)
    {
        int dimensions = num / 4;
        float x = 0;
        float z = 0;
        if (position < dimensions)
        {
            x = position % dimensions * space;
        } else if (position <= dimensions * 3 && position > dimensions * 2)
        {
            x = position % dimensions * space;
            z = dimensions * space;
        } else if (position >= dimensions * 3)
        {
            z = position % dimensions * space;
        } else if (position == dimensions * 2)
        {
            x = dimensions * space;
            z = dimensions * space;
        } else 
        {
            z = position % dimensions * space;
            x = dimensions * space;
        }
        agent.SetDestination(corner + new Vector3(x, 0, z));
    }

    void CircleFormation(Vector3 center, float radius)
    {
        float angle = 360f / num;
        agent.SetDestination(center + new Vector3(Mathf.Cos(angle * position), 0, Mathf.Sin(angle * position)) * radius);
    }
}
