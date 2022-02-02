using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class Computer : MonoBehaviour
{
    string[] words;
    string[] availableWords;
    char[] guessedLetters;
    string actualWord;
    string guessedWord;
    bool myTurn;

    string myWordToGuess;

    [SerializeField] TMP_Text hint;
    [SerializeField] TMP_Text guesses;
    [SerializeField] GameObject[] body;
    [SerializeField] Button restart;
    [SerializeField] TMP_Text gameOverText;
    [SerializeField] Player player;
    
    // Start is called before the first frame update
    void Start()
    {
        StreamReader reader = new StreamReader("Words.txt");
        int size = int.Parse(reader.ReadLine());
        words = new string[size];
        for (int i = 0; i < size; i++)
        {
            words[i] = reader.ReadLine();
        }
        StartGame();
    }

    private void Update()
    {
        if (myTurn)
        {
            MakeGuess();
        }
    }

    public void GuessLetter(string s)
    {
        char l = char.Parse(s.ToUpper());
        for (int i = 0; i < guessedLetters.Length; i++)
        {
            if (guessedLetters[i] == l)
            {
                return;
            }
        }
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
        if (actualWord == guessedWord)
        {
            Win();
        }
        if (!found)
        {
            WrongGuess(l);
        }
        myTurn = true;
    }

    public void StartGame()
    {
        restart.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        guessedLetters = new char[26];
        actualWord = words[Random.Range(0, words.Length)];
        actualWord = actualWord.ToUpper();
        guessedWord = "";
        availableWords = words;
        for (int i = 0; i < actualWord.Length; i++)
        {
            guessedWord += "_";
        }
        for (int i = 0; i < body.Length; i++)
        {
            body[i].gameObject.SetActive(false);
        }
        hint.text = guessedWord;
        GenerateGuess();
    }

    void WrongGuess(char l)
    {
        for (int i = 0; i < guessedLetters.Length; i++)
        {
            if (guessedLetters[i] == 0)
            {
                guessedLetters[i] = l;
                break;
            } else if (guessedLetters[i] > l)
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
                    Lose();
                } else
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

    public void Lose()
    {
        gameOverText.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        gameOverText.text = "You Lose";
    }

    public void Win()
    {
        gameOverText.gameObject.SetActive(true);
        restart.gameObject.SetActive(true);
        gameOverText.text = "You Win";
    }

    public void ReceiveClue(string s)
    {
        myWordToGuess = s;
        hint.text = myWordToGuess;
        for (int i = 0; i < availableWords.Length; i++)
        {
            if (availableWords[i].Length != myWordToGuess.Length)
            {
                availableWords[i] = null;
            }
        }
    }

    void MakeGuess()
    {
        string s = availableWords[0];
        int j = 0;
        while (s == null)
        {
            j++;
            s = availableWords[j];
        }
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] != myWordToGuess[i])
            {
                myWordToGuess = player.MakeGuess(s[i]);
                break;
            }
        }
        FilterGuesses();
        myTurn = false;
    }

    void FilterGuesses()
    {
        for (int i = 0; i < myWordToGuess.Length; i++)
        {
            for (int j = 0; j < availableWords.Length; j++)
            {
                if (availableWords[j] != null && myWordToGuess[i] != '_' && availableWords[j][i] != myWordToGuess[i])
                {
                    availableWords[j] = null;
                }
            }
        }
    }
}
