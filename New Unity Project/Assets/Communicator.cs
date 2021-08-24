using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Communicator : MonoBehaviour
{
    public GameObject GO;
    public TaskRobot robot;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("ToggleGoActive")]
    void ToggleGoActive()
    {
        GO.SetActive(!GO.activeSelf);
    }

    void SendRobotToStore()
    {
        robot.GoToStore();
    }
}
