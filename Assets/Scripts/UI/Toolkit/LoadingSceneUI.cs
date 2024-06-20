using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadingSceneUI : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;
    ProgressBar LoadingProgress;
    private void Start()
    {
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        LoadingProgress = root.Q("LoadingProgress") as ProgressBar;
    }
    private void Update()
    {
        LoadingProgress.value=Loader.GetLoadingProgress();
    }
}
