using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumberWizard : MonoBehaviour {

    private int max;
    private int min;
    private int guess;
    public int maxGuessesAllowed = 5;
    public Text text;
    void Start () {
        StartGame();
	}
	
    void StartGame()
    {
        max = 1000;
        min = 1;
        NextGuess();
    }
    
    public void GuessLower()
    {
        max = guess;
        NextGuess();
    }

    public void GuessHigher()
    {
        min = guess;
        NextGuess();
    }

    void NextGuess()
    {
        guess = Random.Range(min,max+1);
        text.text = guess.ToString();
        maxGuessesAllowed--;
        if (maxGuessesAllowed <= 0)
        {
            Application.LoadLevel("Win");
        }
    }
}
