using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptorFactory : MonoBehaviour
{
    public InterceptorBot bot = new InterceptorBot();
    static int serialNumber = 0;
    public List<InterceptorBot> bots = new List<InterceptorBot>();
    // public InterceptorBot prefab;
 
    // Start is called before the first frame update
    private void Start()
    {
        
    }
    /*
    [ContextMenu("Create Bot")]
    void CreateInterceptor()
    {
        bot = Instantiate(prefab);
    }
    */
    [ContextMenu("Create Bot")]
    void CreatInterceptor()
    {
        serialNumber++;
        GameObject GO = new GameObject();
        GO.name = "InterceptoBot" + serialNumber.ToString();
        bot = GO.AddComponent<InterceptorBot>();
        bots.Add(bot);
    }
}
