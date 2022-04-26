using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minigame : MonoBehaviour
{
    [SerializeField] Button launchNextSceneButton;

    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] Button button3;
    [SerializeField] Button button4;

    [SerializeField] Text instructionText;
    [SerializeField] Text youSuckText;

    // Start is called before the first frame update
    void Start()
    {
        launchNextSceneButton.gameObject.SetActive(false);
        button1.gameObject.SetActive(true);
        button2.gameObject.SetActive(true);
        button3.gameObject.SetActive(true);
        button4.gameObject.SetActive(true);
        instructionText.gameObject.SetActive(true);
        youSuckText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<LoadingManager>().sceneLoaded)
        {
            launchNextSceneButton.gameObject.SetActive(true);
        }
    }

    public void YouSuck()
    {
        button1.gameObject.SetActive(false);
        button2.gameObject.SetActive(false);
        button3.gameObject.SetActive(false);
        button4.gameObject.SetActive(false);
        instructionText.gameObject.SetActive(false);
        youSuckText.gameObject.SetActive(true);
    }

    public void LoadNextScene(int i)
    {
        FindObjectOfType<LoadingManager>().ClickableLoadingScene(i);
    }

    public void LaunchNextScene()
    {
        FindObjectOfType<LoadingManager>().CloseLoadingScene();
    }
}
