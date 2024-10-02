using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
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
    public Label SoftMoneyAdded;
    public Label HardMoney;
    public Label TimeToHealLeft;
    public Label Lifes;
    [SerializeField]
    public List<VisualElement> HeartsList;
    public Button Pause;
    public Button GoToMenu;
    
    private VisualElement PreUpper;
    public AdaptiveText Progress;
    public ProgressBar LevelProgress;


    private VisualElement PlayButtons;
    private VisualElement CatsContainer;
    private VisualElement CatRow1;
    private VisualElement CatRow2;

    private VisualElement Hearts;

    public Button NewGamebtn;
    [SerializeField]
    public Button RepeatSoundsbtn;
    [SerializeField]

    public Toggle Outline;
    [SerializeField]

    private SettingsUI settingsUI;

    private VisualElement SettingsMenu;
    public SequenceGameManager SequenceGameManager;
    private void Awake()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;

        UpperPanel = root.Q("UpperPanel");
        SoftMoney = UpperPanel.Q("Money").Q("MoneyText") as Label;
        SoftMoneyAdded = UpperPanel.Q("Money").Q("MoneyAddedText") as Label;
        SoftMoneyAdded.SetEnabled(false);
        Wallet.moneyChangedBy.AddListener((Money money) => StartCoroutine(SoftMoneyChangedAnimation(money)));
        HardMoney = UpperPanel.Q("Fishes").Q("FishText") as Label; 
        TimeToHealLeft = UpperPanel.Q("HeartsRecovery").Q("TimeText") as Label;
        Lifes = UpperPanel.Q("HeartsRecovery").Q("HealthText") as Label;
        Pause = UpperPanel.Q("Buttons").Q("Pause") as Button;
        GoToMenu = UpperPanel.Q("Buttons").Q("Menu") as Button;

        PreUpper = root.Q("PreUpper");
        Progress = UpperPanel.Q("Progress").Q("LvlText") as AdaptiveText; 
        Progress.OnEnable();
        LevelProgress = UpperPanel.Q("Progress").Q("LvlProgress") as ProgressBar;

        PlayButtons = root.Q("PlayButtons");
        CatsContainer = PlayButtons.Q("CatsContainer");
        CatRow1 = CatsContainer.Q("Row1") as VisualElement;
        CatRow2 = CatsContainer.Q("Row2") as VisualElement;
        int j = 0;
        SetupCatShopItems(CatRow1.Children(), ref j);
        SetupCatShopItems(CatRow2.Children(), ref j);

        Hearts = root.Q("Hearts");
        Debug.Log(Hearts.Children().Count());
        HeartsList = Hearts.Children().ToList();

        RepeatSoundsbtn = root.Q("PlayButtons"). Q("Repeat") as Button;
        NewGamebtn = root.Q("PlayButtons").Q("Play") as Button;

        TimeToHealLeft.text = HealthSystem.TimeLeftString;
        //Lifes.text = HealthSystem.Health.ToString()+"/9";
        Lifes.text = "";
        for (int i = 0;i<9;i++)
        {
            if (i < HealthSystem.Health)
                OnHealthHeal(i);
            else 
                OnHealthLost(i);
        }
        Progress.text = "Уровень " + "1";
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
        HealthSystem.healthLost.AddListener(OnHealthLost);
        HealthSystem.healthHealed.AddListener(OnHealthHeal);
        //Outline = root.Q("Outline") as Toggle;
        //Outline.RegisterCallback<ClickEvent>(evt => OutlineChange(Outline.value));
        SoundSequenceGame.instance.roundStateChanged.AddListener(OnController_RoundStateChanged);
        SoundSequenceGame.instance.roundStateChanged.Invoke(SoundSequenceGame.instance.roundState);

        ScoreManager.instance.ScoreChanged.AddListener(OnScoreChanged);
        //ScoreManager.instance.NewHighScoreReached.AddListener(OnHighScoreReached);

        //SettingsMenu = root.Q("SettingsMenu");
        //SettingsMenu.style.display = DisplayStyle.None;
        //settingsUI.Setup(SettingsMenu);
        Pause.clicked += () =>
        {
            ManageSettingsMenu();
        };

        Debug.Log("GameUI loaded with " + Wallet.Money.SoftMoney.ToString() + "soft money");

    }
    private void Start()
    {
        SoftMoney.text = Wallet.Money.SoftMoney.ToString();
        HardMoney.text = Wallet.Money.HardMoney.ToString();
    }
    private  IEnumerator SoftMoneyChangedAnimation(Money money)
    {
        if (money.SoftMoney > 0)
        {
            SoftMoneyAdded.text = "+";
        }
        else
            SoftMoneyAdded.text = "";
        SoftMoneyAdded.text += money.SoftMoney.ToString();
        SoftMoneyAdded.SetEnabled(true);
        yield return new WaitForSeconds(1f);
        SoftMoneyAdded.SetEnabled(false);
        SoftMoney.text = Wallet.Money.SoftMoney.ToString();
    }

    private void OnHealthHeal(int arg0)
    {
        if (arg0 > 0)
        HeartsList[arg0-1].SetEnabled(true);
        //Lifes.text = arg0.ToString() + "/9";
    }

    private void OnHealthLost(int arg0)
    {
        if(arg0 < HeartsList.Count)
        HeartsList[arg0].SetEnabled(false);
        //Lifes.text = arg0.ToString() + "/9";
    }

    private void ManageSettingsMenu()
    {
        if (SettingsMenu.style.display == DisplayStyle.Flex)
            SettingsMenu.style.display = DisplayStyle.None;
        else
            SettingsMenu.style.display = DisplayStyle.Flex;
        
    }
    private void Update()
    {
        HardMoney.text = Wallet.Money.HardMoney.ToString();
        TimeToHealLeft.text = HealthSystem.TimeLeftString;
        if(SoundSequenceGame.instance.GetCatSequenceLength()!=0)
            LevelProgress.value = SoundSequenceGame.instance.CurrentNum * 100 / SoundSequenceGame.instance.GetCatSequenceLength();
    }
    private void OnScoreChanged(int score)
    {
        Debug.Log("dcore changed");
        Progress.text = "Уровень " +score.ToString();
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
    private void SetupCatShopItems(IEnumerable<VisualElement> children, ref int j)
    {
        List<Cat> cats = SequenceGameManager.GetCats();
        Debug.Log(cats.Count);
        foreach (VisualElement item in children)
        {
            Button catBtn = item.Q<Button>();
            Cat cat = cats[j];
            Debug.Log(cat.name);
            cat.Init(catBtn);
            catBtn.clicked += () => { SequenceGameManager.OnCatClick(cat); };
            //Button activate = buttons.Q("Activate") as Button;
            j++;
        }
    }
}
