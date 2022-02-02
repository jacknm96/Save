using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    string actualWord;
    string guessedWord;
    char[] guessedLetters;

    [SerializeField] TMP_Text hint;
    [SerializeField] TMP_Text guesses;
    [SerializeField] GameObject[] body;
    [SerializeField] Computer computer;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        guessedLetters = new char[26];
        for (int i = 0; i < body.Length; i++)
        {
            body[i].gameObject.SetActive(false);
        }
        hint.text = guessedWord;
        GenerateGuess();
    }

    public void SubmitWord(string s)
    {
        actualWord = s;
        guessedWord = "";
        for (int i = 0; i < actualWord.Length; i++)
        {
            guessedWord += "_";
        }
        computer.ReceiveClue(guessedWord);
    }

    public string MakeGuess(char l)
    {
        bool found = false;
        System.Text.StringBuilder strB = new System.Text.StringBuilder(guessedWord);
        for (int i = 0; i < actualWord.Length; i++)
        {
            if (actualWord[i] == l)
            {
                strB[i] = l;
                found = true;
            }
        }
        guessedWord = strB.ToString();
        hint.text = guessedWord;
        if (!found)
        {
            WrongGuess(l);
        }
        return guessedWord;
    }

    void WrongGuess(char l)
    {
        for (int i = 0; i < guessedLetters.Length; i++)
        {
            if (guessedLetters[i] == 0)
            {
                guessedLetters[i] = l;
                break;
            }
            else if (guessedLetters[i] > l)
            {
                for (int j = guessedLetters.Length - 1; j > i; j--)
                {
                    guessedLetters[j] = guessedLetters[j - 1];
                }
                guessedLetters[i] = l;
                break;
            }
        }
        GenerateGuess();
        for (int i = 0; i < body.Length; i++)
        {
            if (!body[i].activeSelf)
            {
                body[i].gameObject.SetActive(true);
                if (i == body.Length - 1)
                {
                    computer.Win();
                }
                else
                {
                    break;
                }
            }
        }
    }

    void GenerateGuess()
    {
        string guess = "";
        for (int i = 0; i < guessedLetters.Length; i++)
        {
            guess += guessedLetters[i] + " ";
        }
        guesses.text = guess;
    }
}
