using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotateSpeed;

    float direction;
    float rotateDirection;
    Rigidbody rb;
    
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
        rb.velocity = transform.forward * direction * moveSpeed;
        transform.Rotate(new Vector3(0, rotateDirection * rotateSpeed, 0));
    }

    private void OnMove(InputValue value)
    {
        direction = value.Get<float>();
    }

    private void OnTurn(InputValue value)
    {
        rotateDirection = value.Get<float>();
    }
}
