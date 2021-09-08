using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopMovement : CityMovement
{
    GameObject player;
    
    override public void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public override void Movement()
    {
        transform.Rotate();
    }
}
