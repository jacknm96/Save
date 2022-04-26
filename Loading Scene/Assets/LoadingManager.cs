using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager instance;

    public bool sceneLoaded;
    int prevSceneIndex;
    int sceneToLoad;

    [SerializeField] int loadingSceneBuildIndex;

    [SerializeField] public static Image progressBar;
    

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        progressBar = FindObjectOfType<Image>();
    }

    public void LaunchNextScene()
    {
        
    }

    public void LoadScene(int i)
    {
        StartCoroutine(LoadSceneAsync(i));
    }

    IEnumerator LoadSceneAsync(int i)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(i);
        while (!operation.isDone)
        {
            progressBar.fillAmount = operation.progress;
            yield return null;
        }
    }

    public void ClickableLoadingScene(int i)
    {
        prevSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ClickableLoadScene(i));
    }

    IEnumerator ClickableLoadScene(int i)
    {
        AsyncOperation operation =SceneManager.LoadSceneAsync(loadingSceneBuildIndex, LoadSceneMode.Additive);
        Debug.Log(" Current scene is " + SceneManager.GetActiveScene().name);
        //SceneManager.UnloadScene(SceneManager.GetActiveScene());
        Debug.Log(" after unloading current scene is " + SceneManager.GetActiveScene().name);
        yield return new WaitUntil(() => operation.isDone);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(loadingSceneBuildIndex));
        sceneToLoad = i;
        operation = SceneManager.LoadSceneAsync(i, LoadSceneMode.Additive);
        while (!operation.isDone)
        {
            progressBar.fillAmount = operation.progress;
            yield return null;
        }
        SceneManager.UnloadScene(prevSceneIndex);
        sceneLoaded = true;
    }

    public void CloseLoadingScene()
    {
        if (sceneLoaded)
        {
            SceneManager.UnloadSceneAsync(loadingSceneBuildIndex);
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneToLoad));
            sceneLoaded = false;
        }
    }
}
