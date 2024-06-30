using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class GameUi : MonoBehaviour
{

    private UIDocument document;
    private VisualElement root;
    [SerializeField]
    private Wallet Wallet;

    private VisualElement UpperPanel;
    public Label SoftMoney;
    public Label HardMoney;
    public Label TimeToHealLeft;
    public Label Lifes;
    public Button Pause;
    public Button GoToMenu;
    
    private VisualElement PreUpper;
    public Label ScoreText;
    public ProgressBar LevelProgress;


    private VisualElement CatsContainer;
    private VisualElement Row1;
    private VisualElement Row2;

    private VisualElement Hearts;

    public Button NewGamebtn;
    public Button RepeatSoundsbtn;
    [SerializeField]
    public Toggle Outline;
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;

        UpperPanel = root.Q("UpperPanel");
        SoftMoney = UpperPanel.Q("Money").Q("MoneyText") as Label;
        HardMoney = UpperPanel.Q("Fishes").Q("FishText") as Label; 
        TimeToHealLeft = UpperPanel.Q("HeartsRecovery").Q("TimeText") as Label;
        Lifes = UpperPanel.Q("HeartsRecovery").Q("HealthText") as Label;
        Pause = UpperPanel.Q("Buttons").Q("Pause") as Button;
        GoToMenu = UpperPanel.Q("Buttons").Q("Menu") as Button;

        PreUpper = root.Q("PreUpper");
        ScoreText = PreUpper.Q("Progress").Q("LvlText") as Label;
        LevelProgress = PreUpper.Q("Progress").Q("LvlProgress") as ProgressBar;

        CatsContainer = root.Q("CatsContainer");
        Row1 = CatsContainer.Q("Row1");
        Row2 = CatsContainer.Q("Row2");

        Hearts = root.Q("Hearts");

        RepeatSoundsbtn = root.Q("PlayButtons"). Q("Repeat") as Button;
        NewGamebtn = root.Q("PlayButtons").Q("Play") as Button;

        SoftMoney.text = Wallet.Money.SoftMoney.ToString();
        HardMoney.text = Wallet.Money.HardMoney.ToString();
        TimeToHealLeft.text = HealthSystem.TimeLeftString;
        Lifes.text = HealthSystem.Health.ToString()+"/9";

        ScoreText.text = "Уровень " + "1";
        LevelProgress.value = 10;

        NewGamebtn.clicked += () =>
        {
            SoundSequenceGame.instance.StartNewGame();
        };
        RepeatSoundsbtn.clicked += () =>
        {
            SoundSequenceGame.instance.StartRound();
        };
        GoToMenu.clicked += () =>
        {
            Loader.Load(Loader.Scene.MainMenu);
        };
        HealthSystem.healthChanged.AddListener(OnHealthChange);
        //Outline = root.Q("Outline") as Toggle;
        //Outline.RegisterCallback<ClickEvent>(evt => OutlineChange(Outline.value));
        SoundSequenceGame.instance.roundStateChanged.AddListener(OnController_RoundStateChanged);
        ScoreManager.instance.ScoreChanged.AddListener(OnScoreChanged);
        //ScoreManager.instance.NewHighScoreReached.AddListener(OnHighScoreReached);
    }
    private void OnHealthChange(int arg0)
    {
        Lifes.text = HealthSystem.Health.ToString()+"/9";
    }
    private void Update()
    {
        SoftMoney.text = Wallet.Money.SoftMoney.ToString();
        HardMoney.text = Wallet.Money.HardMoney.ToString();
        TimeToHealLeft.text = HealthSystem.TimeLeftString;
        if(SoundSequenceGame.instance.CatSequence.Count!=0)
            LevelProgress.value = SoundSequenceGame.instance.CurrentNum * 100 / SoundSequenceGame.instance.CatSequence.Count;
    }
    private void OnScoreChanged(int score)
    {
        Debug.Log("dcore changed");
        ScoreText.text = "Уровень " +(score+1).ToString();
    }
    private void OnHighScoreReached()
    {
        //ScoreText.text = "New High Score!! \n" + ScoreText.text;
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
