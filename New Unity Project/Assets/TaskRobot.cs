using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskRobot : MonoBehaviour
{
    public bool canGoToStore;
    public Transform storeLocation;
    [SerializeField] float speed;
    public SportTypes favoriteSport;
    public int? stockMarketIndexPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Should I Sell?")]
    void ShouldISell()
    {
        if (stockMarketIndexPoint == null)
        {
            Debug.Log("Don't Sell");
        } else
        {
            Debug.Log(stockMarketIndexPoint.ToString());
        }
    }

    private void FixedUpdate()
    {
        if (canGoToStore)
        {
            GoToStore();
        }
    }
    [ContextMenu("Check if Robot can go to Store")]

    void IsRobotBusy()
    {
        canGoToStore = Random.Range(0, 2) > 0;
    }

    [ContextMenu("Tell Robot to Go to Store")]
    public void GoToStore()
    {
        Debug.Log(canGoToStore ? "Robot Going To Store" : "Robot Can't Go To Store");
        if (canGoToStore)
        {
            transform.LookAt(storeLocation);
            Debug.Log("Robot looking at store");
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
    }
}
