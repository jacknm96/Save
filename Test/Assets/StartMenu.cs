using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMenu : MonoBehaviour
{
    public TMP_Text text;
    [SerializeField] PlayerMovement2D player;
    [SerializeField] Timer canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        text.text = "Start Game?";
    }

    public void StartGame()
    {
        canvas.TurnOn();
        player.Respawn();
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        text.text = "Restart?";
        gameObject.SetActive(true);
    }
}
