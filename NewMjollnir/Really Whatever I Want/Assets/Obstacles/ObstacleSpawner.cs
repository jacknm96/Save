using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] int difficulty;
    [SerializeField] GameObject prefab;
    ObjectPool pool;
    
    // Start is called before the first frame update
    void Start()
    {
        pool = new ObjectPool();
        StartCoroutine(Spawner());
    }

    void CreateObstacles()
    {
        List<int> holders = new List<int>();
        for (int i = 0; i < difficulty; i++)
        {
            int pos = (int)Random.Range(0, 9);
            while (holders.Contains(pos))
            {
                pos = (int)Random.Range(0, 9);
            }
            //int pos = 8;
            holders.Add(pos);
            ObjectPool.Spawn(prefab, new Vector3((-7f/3f) + ((7f/3f) * (pos % 3)), (-7f / 3f) + ((7f / 3f) * (pos / 3)), 50f));
        }
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            CreateObstacles();
            yield return new WaitForSeconds(5);
        }
    }
}
