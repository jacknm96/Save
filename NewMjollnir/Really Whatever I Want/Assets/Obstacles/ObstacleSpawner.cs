using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] int difficulty;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void CreateObstacles()
    {

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
