using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;
    private Button Play;
    private void Start()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        Play = root.Q("Play") as Button;
        Play.clicked += () =>
        {
            Debug.Log("play clicke");
            Loader.Load(Loader.Scene.Game);
        };
    }
    
}
