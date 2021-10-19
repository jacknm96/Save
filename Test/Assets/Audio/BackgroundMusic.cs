using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] UnityEvent audio1;
    [SerializeField] UnityEvent audio2;
    [SerializeField] UnityEvent audio3;
    [SerializeField] AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ClipThree());
    }

    IEnumerator ClipOne()
    {
        audio1.Invoke();
        yield return new WaitForSeconds(280);
        StartCoroutine(ClipTwo());
    }

    IEnumerator ClipTwo()
    {
        audio2.Invoke();
        yield return new WaitForSeconds(170);
        StartCoroutine(ClipThree());
    }

    IEnumerator ClipThree()
    {
        audio3.Invoke();
        yield return new WaitForSeconds(120);
        float startTime = Time.time;
        while (Time.time - startTime < 10)
        {
            audioSource.volume = Mathf.Lerp(0.8f, 0f, 10 * Time.deltaTime);
            yield return null;
        }
        StartCoroutine(ClipOne());
    }
}
