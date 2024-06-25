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
    [SerializeField]
    private Wallet Wallet;
    public Label ScoreText;
    public Label SoftMoney;
    public Label HardMoney;
    public Label TimeToHealLeft;
    public Label Lifes;
    public Button NewGamebtn;
    public Button RepeatSoundsbtn;
    public Button GoToMenu;
    [SerializeField]
    public Toggle Outline;
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        ScoreText = root.Q("ScoreText") as Label;
        TimeToHealLeft = root.Q("TimeToHealLeft") as Label;
        Lifes = root.Q("Lifes") as Label;

        NewGamebtn = root.Q("NewGame") as Button;
        NewGamebtn.clicked += () =>
        {
            SoundSequenceGame.instance.StartNewGame();
        };

        RepeatSoundsbtn = root.Q("RepeatSounds") as Button;
        RepeatSoundsbtn.clicked += () =>
        {
            SoundSequenceGame.instance.StartRound();
        };
        GoToMenu = root.Q("GoToMenu") as Button;
        GoToMenu.clicked += () =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        };
        Lifes.text = HealthSystem.Health.ToString();
        HealthSystem.healthChanged.AddListener(OnHealthChange);
        Outline = root.Q("Outline") as Toggle;
        Outline.RegisterCallback<ClickEvent>(evt => OutlineChange(Outline.value));
        SoundSequenceGame.instance.roundStateChanged.AddListener(OnController_RoundStateChanged);
        ScoreManager.instance.ScoreChanged.AddListener(OnScoreChanged);
        ScoreManager.instance.NewHighScoreReached.AddListener(OnHighScoreReached);
    }
    private void OnHealthChange(int arg0)
    {
        Lifes.text = HealthSystem.Health.ToString();
    }
    private void Update()
    {
        TimeToHealLeft.text = HealthSystem.TimeLeftString;
    }
    private void OnScoreChanged(int score)
    {
        ScoreText.text = score.ToString();
    }
    private void OnHighScoreReached()
    {
        ScoreText.text = "New High Score!! \n"+ ScoreText.text;
    }
    private void OnController_RoundStateChanged(SoundSequenceGame.RoundState state)
    {
        switch (state)
        {
            case SoundSequenceGame.RoundState.gameStarting:
                {
                    NewGamebtn.SetEnabled(true);
                    RepeatSoundsbtn.SetEnabled(false);
                    break;
                }
            case SoundSequenceGame.RoundState.playingSounds:
                {
                    NewGamebtn.SetEnabled(false);
                    RepeatSoundsbtn.SetEnabled(false);
                    break;
                };
            case SoundSequenceGame.RoundState.roundStarting:
                {
                    NewGamebtn.SetEnabled(false);
                    RepeatSoundsbtn.SetEnabled(true);
                    break;
                }
            case SoundSequenceGame.RoundState.playing:
                {
                    NewGamebtn.SetEnabled(false);
                    RepeatSoundsbtn.SetEnabled(false);
                    break;
                }
            
            case SoundSequenceGame.RoundState.lost:
                {
                    NewGamebtn.SetEnabled(true);
                    RepeatSoundsbtn.SetEnabled(true);
                    break;
                }
            
            
        }

    }
    private void OnDisable()
    {
        SoundSequenceGame.instance.roundStateChanged.RemoveListener(OnController_RoundStateChanged);
    }
    private void OutlineChange(bool b)
    {
        if (b)
            Outlinev2.state = Outlinev2.OutlineState.show;
        else
            Outlinev2.state = Outlinev2.OutlineState.hide;
    }
}
