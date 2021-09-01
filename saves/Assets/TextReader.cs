using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class TextReader : MonoBehaviour
{
    [SerializeField] TMP_Text inputField;

    public void PassString()
    {
        WriteThing(inputField.text);
    }

    public static void WriteThing(string thing)
    {
        StreamWriter writer = new StreamWriter("text.txt");
        writer.WriteLine(thing);
        writer.Close();
    }
}
