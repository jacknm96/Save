using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDirection : MonoBehaviour
{
    public Vector3 myPos;
    [SerializeField] Transform follow;
    
    // Start is called before the first frame update
    void Start()
    {
        myPos = transform.position - follow.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.position + myPos;
        transform.LookAt(follow);
    }
}
