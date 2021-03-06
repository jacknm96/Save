using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] int difficulty;
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject fuel;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        ObjectPool.CreatePool(prefab, difficulty);
        ObjectPool.CreatePool(fuel, 1);
        StartCoroutine(Spawner());
    }

    void CreateObstacles()
    {
        List<int> holders = new List<int>{0, 1, 2, 3, 4, 5, 6, 7, 8 };
        int num = 0;
        int pos = 0;
        for (int i = 0; i < difficulty; i++)
        {
            num = (int)Random.Range(0, holders.Count);
            pos = holders[num];
            holders.RemoveAt(num);
            ObjectPool.Spawn(prefab, new Vector3((-7f/3f) + ((7f/3f) * (pos % 3)), (-7f / 3f - 5f / 6f) + ((7f / 3f) * (pos / 3)), 50f));
        }
        if ((int)Random.Range(0, 5) == 0)
        {
            num = (int)Random.Range(0, holders.Count);
            pos = holders[num];
            ObjectPool.Spawn(fuel, new Vector3((-7f / 3f) + ((7f / 3f) * (pos % 3)), (-7f / 3f) + ((7f / 3f) * (pos / 3)), 50f));
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
            CreateObstacles();
            if (Time.time - startTime > 15 && waitTime > 1)
            {
                waitTime -= 0.2f;
                startTime = Time.time;
            }
            yield return new WaitForSeconds(waitTime * Random.Range(0.8f, 1.2f));
        }
    }
}
