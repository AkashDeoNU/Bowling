using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public static Scoreboard Singleton;
    public AudioSource win;
    public AudioSource lose;
            
    public static void ScorePoints(int points)
    {
        Singleton.ScorePointsInternal(points);
    }
    public static void GameOver()
    {
        Singleton.GameOverInternal();
    }

    public static void GameWin()
    {
        Singleton.GameWinInternal();
    }

    private static int Score;
    public static int round = 0;
    public int GameWinScore; 
    private TMP_Text scoreDisplay;

    // Start is called before the first frame update
    void Start()
    {
        Singleton = this;
        scoreDisplay = GetComponent<TMP_Text>();
        GameWinScore = 40;
        // Initialize the display
        ScorePointsInternal(0);
    }

    private void ScorePointsInternal(int points)
    {
        Score += points;
        if (Score >= GameWinScore)
        {
            GameWin();
            win.Play();
        }
        else if (round >= 5)
        {
            GameOver();
            lose.Play();
        }
        else
        {
            scoreDisplay.text = "Score: " + Score.ToString();
        }
    }

    private void GameOverInternal()
    {
        Time.timeScale = 0;
        scoreDisplay.text = "You Lose / OOPS";

    }

    private void GameWinInternal()
    {
        Time.timeScale = 0;
        scoreDisplay.text = "You Win!";
    }
}
