using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeAudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider volumeSlider;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        float audioLevel;
        audioMixer.GetFloat("MasterVolume", out audioLevel);
        volumeSlider.value = audioLevel;
    }

    // Update is called once per frame
    public void UpdateAudio()
    {
        audioMixer.SetFloat("MasterVolume", volumeSlider.value);
    }
}
