using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager: MonoBehaviour
{
    public static ScoreManager instance;
    public int Score=0;
    public int HighScore=0;
    public UnityEvent <int> ScoreChanged;
    public UnityEvent NewHighScoreReached;
    private void Awake()
    {
        instance = this;
    }
    public void Add()
    {
        Score++;
        ScoreChanged.Invoke(Score);
        if(Score > HighScore)
        {
            OnNewHighScore();
        }
    }
    public void Set(int score)
    {
        Score=score;
        ScoreChanged.Invoke(Score);
        if (Score > HighScore)
        {
            OnNewHighScore();
        }
    }

    private void OnNewHighScore()
    {
        HighScore = Score;
        PlayerPrefs.SetInt("HighScore", Score);
        NewHighScoreReached.Invoke();
    }

    public void ResetPoints()
    {
        Score = 0;
        ScoreChanged.Invoke(Score);
    }
}
