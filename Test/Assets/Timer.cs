using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] StartMenu button;
    [SerializeField] TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameTimer());
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);
        Start();
    }

    IEnumerator GameTimer()
    {
        float timer = Time.time;
        while (Time.time - timer < 10)
        {
            text.text = ((int)(10 - (Time.time - timer))).ToString();
            yield return null;
        }
        gameObject.SetActive(false);
        button.Restart();
    }
}
