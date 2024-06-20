using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GameUi : MonoBehaviour
{

    private UIDocument document;
    private VisualElement root;
#if true==useToolkit
    [SerializeField]
#endif
    public Label ScoreText;
    [SerializeField]
    public Button NewGamebtn;
    [SerializeField]
    public Button RepeatSoundsbtn;
    [SerializeField]
    public Toggle Outline;
    public bool useToolkit;
    private void Awake()
    {
        if (useToolkit)
        {
            document = GetComponent<UIDocument>();
            root = document.rootVisualElement;
            ScoreText = root.Q("ScoreText") as Label;

            NewGamebtn = root.Q("NewGame") as Button;
            NewGamebtn.clicked += () =>
            {
                SoundQueueController.instance.StartNewGame();
            };

            RepeatSoundsbtn = root.Q("RepeatSounds") as Button;
            RepeatSoundsbtn.clicked += () =>
            {
                SoundQueueController.instance.StartRound();
            };

            Outline = root.Q("Outline") as Toggle;
        }
        Outline.RegisterCallback<ClickEvent>(evt => OutlineChange(Outline.value));
        SoundQueueController.instance.roundStateChanged.AddListener(OnController_RoundStateChanged);
        ScoreManager.instance.ScoreChanged.AddListener(OnScoreChanged);
        ScoreManager.instance.NewHighScoreReached.AddListener(OnHighScoreReached);
    }
    private void OnScoreChanged(int score)
    {
        ScoreText.text = score.ToString();
    }
    private void OnHighScoreReached()
    {
        ScoreText.text = "New High Score!! \n"+ ScoreText.text;
    }
    private void OnController_RoundStateChanged(SoundQueueController.RoundState state)
    {
        switch (state)
        {
            case SoundQueueController.RoundState.gameStarting:
                {
                    NewGamebtn.SetEnabled(true);
                    RepeatSoundsbtn.SetEnabled(false);
                    break;
                }
            case SoundQueueController.RoundState.playingSounds:
                {
                    NewGamebtn.SetEnabled(false);
                    RepeatSoundsbtn.SetEnabled(false);
                    break;
                };
            case SoundQueueController.RoundState.roundStarting:
                {
                    NewGamebtn.SetEnabled(false);
                    RepeatSoundsbtn.SetEnabled(true);
                    break;
                }
            case SoundQueueController.RoundState.playing:
                {
                    NewGamebtn.SetEnabled(false);
                    RepeatSoundsbtn.SetEnabled(false);
                    break;
                }
            
            case SoundQueueController.RoundState.lost:
                {
                    NewGamebtn.SetEnabled(true);
                    RepeatSoundsbtn.SetEnabled(false);
                    break;
                }
            
            
        }

    }
    private void OnDisable()
    {
        SoundQueueController.instance.roundStateChanged.RemoveListener(OnController_RoundStateChanged);
    }
    private void OutlineChange(bool b)
    {
        if (b)
            Outlinev2.state = Outlinev2.OutlineState.show;
        else
            Outlinev2.state = Outlinev2.OutlineState.hide;
    }
}
