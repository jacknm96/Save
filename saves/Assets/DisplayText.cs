using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class DisplayText : MonoBehaviour
{
    [SerializeField] TMP_Text inputField;

    // Start is called before the first frame update
    void Start()
    {
        StreamReader reader = new StreamReader("text.txt");
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            inputField.text = line;
        }
        reader.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
