using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;
[RequireComponent(typeof(AudioSource))]
public class Cat : MonoBehaviour
{
    public CatInfoSO shopItem;
    public bool OutlineEnabled;
    [HideInInspector]
    public AudioClip MeowSound;
    private AudioSource AudioSource;
    private Sprite OutlineSprite;
    private Sprite NoOutlineSprite;
    private Image Image;
    public UnityEvent Clicked;
    private Button button;
    public void Init(CatInfoSO shopItemSO)
    {
        shopItem = shopItemSO;
        OutlineSprite = shopItemSO.OutlineSprite;
        NoOutlineSprite = shopItemSO.Sprite;
        MeowSound = shopItemSO.MeowSound;
        GetComponent<Image>().sprite = NoOutlineSprite;
    }
    public void SetOutlineMode(bool b)
    {
        OutlineEnabled = b;
    }
    public void SetUnclickable(bool b)
    {
        button.enabled = b;
    }
    public void OnClick()
    {
        Debug.Log("ButtonClicked");
        Play();
        Clicked.Invoke();
    }
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();   
        SetOutlineMode(true);
    }
    public void Awake()
    {
        Image = GetComponent<Image>();
        button = GetComponent<Button>();
        Init(shopItem);

    }
    public void Play()
    {
        AudioSource.PlayOneShot(MeowSound);
        ShowOutline();
    }
   
    private void ShowOutline() 
    {
        if (!OutlineEnabled) return;
        if(Outlinev2.state==Outlinev2.OutlineState.show)
        { //if (Outline.ShowOutline == true)
            //    Outline.enabled = true;
            Image.sprite = OutlineSprite;
            Invoke("HideOutline", MeowSound.length);
        }
    }
    private void HideOutline()
    {
        Image.sprite = NoOutlineSprite;
    }
}
