using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge : MonoBehaviour
{
    public Node target;
    public Node root;

    [SerializeField] LineRenderer line;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target.IsActive())
        {
            ActiveLine();
        }
    }

    public void SetNodes(Node start, Node end)
    {
        root = start;
        target = end;
        line.startWidth = 3;
        line.endWidth = 3;
        line.startColor = Color.grey;
        line.endColor = Color.grey;
        line.SetPosition(0, root.transform.position);
        line.SetPosition(1, target.transform.position);
    }

    public void ActiveLine()
    {
        line.startColor = Color.blue;
        line.endColor = Color.blue;
    }
}
