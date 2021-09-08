using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CityMovement
{
    // Update is called once per frame
    override public void Movement()
    {
        Vector3 direction = Vector3.zero;
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
        {
            transform.Rotate(Vector3.up, rotation * Input.GetAxis("Horizontal") * Time.deltaTime);
        }
        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f)
        {
            rb.velocity = transform.forward * Input.GetAxis("Vertical") * (Input.GetKey(KeyCode.LeftShift)? sprint: speed) * Time.deltaTime;
        }
    }
}
