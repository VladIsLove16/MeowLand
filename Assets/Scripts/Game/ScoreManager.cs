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
            Debug.Log("Score Changed Invoke");
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
        SoundSequenceGame.instance.roundStateChanged.AddListener(OnSoundQueueController_StateChanged);
        //SoundSequenceGame.instance.RoundWon.AddListener(Set);
    }
    private void OnSoundQueueController_StateChanged(SoundSequenceGame.RoundState state)
    {
        switch (state)
        {
            case SoundSequenceGame.RoundState.roundStarting: 
                Set(SoundSequenceGame.instance.CatSequence.Count) ;
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
