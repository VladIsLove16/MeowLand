using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager: MonoBehaviour
{
    public static ScoreManager instance;
    private int score=0;
    public int Score
    {
        get { return score; }
        set {
            score = value; ScoreChanged.Invoke(value); 
        }
    }
    private int highScore = 0;
    public int HighScore
    {
        get { return highScore; }
        set {
            
        }
    }
    public UnityEvent <int> ScoreChanged;
    public UnityEvent NewHighScoreReached;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        SoundSequenceController.instance.roundStateChanged.AddListener(OnSoundQueueController_StateChanged);
        SoundSequenceController.instance.RoundWon.AddListener(Set);
    }
    private void OnSoundQueueController_StateChanged(SoundSequenceController.RoundState state)
    {
        switch (state)
        {
            case SoundSequenceController.RoundState.gameStarting:
                ResetPoints();
                break;

        }
    }
    public void Add()
    {
        Score++;
        if(Score > HighScore)
        {
            OnNewHighScore();
        }
    }
    public void Set(int score)
    {
        Score=score;
        if (Score > HighScore)
        {
            OnNewHighScore();
        }
    }

    private void OnNewHighScore()
    {
        HighScore = Score;
        PlayerPrefs.SetInt("HighScore", Score);
    }

    public void ResetPoints()
    {
        Score = 0;
    }
}
