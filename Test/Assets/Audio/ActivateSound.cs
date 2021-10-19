using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSound : MonoBehaviour
{
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void PlayClip()
    {
        audioSource.Play(); // plays the audio clip - cannot stack
        audioSource.PlayOneShot(audioSource.clip); // creates a virtual audiosource that plays the clip (lets you stack audio)
    }

    
}
