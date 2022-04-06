using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Tower : MonoBehaviour
{
    [SerializeField] Bullet prefab;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] float timeBetweenShots;

    Bullet[] bulletPool;
    public List<GameObject> nearby;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletPool = new Bullet[5];
        for (int i = 0; i < bulletPool.Length; i++)
        {
            bulletPool[i] = Instantiate(prefab);
            bulletPool[i].SetHome(this);
            bulletPool[i].gameObject.SetActive(false);
        }
        StartCoroutine(Defend());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireShot()
    {
        for (int i = 0; i < bulletPool.Length; i++)
        {
            if (!bulletPool[i].gameObject.activeSelf)
            {
                bulletPool[i].gameObject.SetActive(true);
                bulletPool[i].transform.position = spawnPoint.transform.position;
                break;
            }
        }
    }

    public void RecycleBullet(Bullet b)
    {
        for (int i = 0; i < bulletPool.Length; i++)
        {
            if (bulletPool[i] == b)
            {
                bulletPool[i].gameObject.SetActive(false);
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Bullet>())
        {
            nearby.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Bullet>())
        {
            RecycleBullet(other.GetComponent<Bullet>());
        }
    }

    IEnumerator Defend()
    {
        while (true)
        {
            GameObject target = gameObject;
            float furthestZ = float.MinValue;
            foreach(GameObject o in nearby)
            {
                if (o.transform.position.z > furthestZ)
                {
                    furthestZ = o.transform.position.z;
                    target = o;
                }
            }
            if (target != gameObject)
            {
                Vector3 lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                transform.rotation = Quaternion.LookRotation(lookPos);
                FireShot();
            }
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }
}
