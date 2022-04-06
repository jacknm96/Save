using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingAnimation : MonoBehaviour
{
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMoveUp(InputValue button)
    {
        anim.SetFloat("forward speed", button.Get<float>());
    }

    private void OnMoveSide(InputValue button)
    {
        anim.SetFloat("side speed", button.Get<float>());
    }
}
