using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    
    private UIDocument document;
    private VisualElement root;
    private VisualElement UpperPanel;
    private VisualElement CenterPanel;
    private VisualElement DownPanel;
    private VisualElement SettingsMenu;


    private Button MyVK;
    private Button MyTG;
    private Button Play;
    private Button SettingsBtn;
    private Button GoToShop;
    private Button Exit;
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private SettingsUI settingsUI;

    private void Start()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        //UpperPanel = root.Q("UpperPanel");
        //CenterPanel = root.Q("CenterPanel");
        //DownPanel = root.Q("DownPanel");
        Play = root.Q("A").Q("B").Q("Play") as Button;
        Play.clicked += () =>
        {
            Debug.Log("play clicke");
            Loader.Load(Loader.Scene.Game);
        };
        MyVK = root.Q("A").Q("B").Q("MyVK") as Button;
        MyVK.clicked += () =>
        {
            Application.OpenURL("https://vk.com/hochu_microbov");
        };
        MyTG = root.Q("A").Q("B").Q("MyTG") as Button;
        MyTG.clicked += () =>
        {
            Application.OpenURL("https://t.me/MeowLand_Vladislove");
        };
        GoToShop = root.Q("A").Q("B").Q("Shop") as Button;
        GoToShop.clicked += () =>
        {
            Loader.Load(Loader.Scene.Shop);
        };
        Exit = root.Q("A").Q("B").Q("Exit") as Button;
        Exit.clicked += () =>
        {
            Application.Quit();
        };
        SettingsMenu = root.Q("SettingsMenu");
        SettingsMenu.style.display = DisplayStyle.None;
        settingsUI.Setup(SettingsMenu); 
        SettingsBtn = root.Q("A").Q("B").Q("Settings") as Button;
        SettingsBtn.clicked += () =>
        {
            ManageSettingsMenu();
        };
    }
    private void ManageSettingsMenu()
    {
        if(SettingsMenu.style.display == DisplayStyle.Flex )
            SettingsMenu.style.display = DisplayStyle.None;
        else
            SettingsMenu.style.display = DisplayStyle.Flex;
    }
}
