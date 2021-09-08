using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{
    [SerializeField] GameObject[] buildings;
    [SerializeField] float distanceX = 30f;
    [SerializeField] float distanceY = 30f;
    [SerializeField] int citySizeX;
    [SerializeField] int citySizeY;
    [SerializeField] float noBuildingChance;

    // Start is called before the first frame update
    void Start()
    {
        BuildCity();
    }

    // Update is called once per frame
    void BuildCity()
    {
        for (int i = 0; i < citySizeX; i++)
        {
            for (int j = 0; j < citySizeY; j++)
            {
                if (Random.Range(0f, 100f) > noBuildingChance)
                {
                    Instantiate(buildings[Random.Range(0, buildings.Length)], transform.position + new Vector3(distanceX * i, 0, distanceY * j), Quaternion.identity);
                }
            }
        }
    }
}
