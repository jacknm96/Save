using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    int difficulty = 100;
    [SerializeField] GameObject prefab;

    // Start is called before the first frame update
    void OnEnable()
    {
        ObjectPool.CreatePool(prefab, difficulty);
        CreateObstacles(0, 100);
        StartCoroutine(Spawner());
    }

    void CreateObstacles(int min, int max)
    {
        for (int i = 0; i < difficulty; i++)
        {
            int x = Random.Range(-100, 100);
            int y = Random.Range(-100, 100);
            int z = Random.Range(min, max);
            while (x > -5 && x < 5 && y > -5 && y < 5)
            {
                x = Random.Range(-100, 100);
                y = Random.Range(-100, 100);
            }
            ObjectPool.Spawn(prefab, new Vector3(x, y, z));
        }
    }

    public void SetDifficulty(int diff)
    {
        difficulty = diff;
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    public void Restart()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        float startTime = Time.time;
        float waitTime = 4;
        while (true)
        {
            CreateObstacles(80, 100);
            if (Time.time - startTime > 15 && waitTime > 1)
            {
                waitTime -= 0.2f;
                startTime = Time.time;
            }
            yield return new WaitForSeconds(waitTime * Random.Range(0.8f, 1.2f));
        }
    }
}
