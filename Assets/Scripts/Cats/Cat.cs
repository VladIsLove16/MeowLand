using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Cat : MonoBehaviour
{
    public ShopItem shopItem;
    public bool OutlineEnabled;
    public AudioClip MeowSound;
    private AudioSource AudioSource;
    [SerializeField]
    private Sprite OutlineSprite;
    [SerializeField]
    private Sprite NoOutlineSprite;
    private Image Image;
    public UnityEvent Clicked;
    private Button button;
    public void SetOutlineMode(bool b)
    {
        OutlineEnabled = b;
        //if (Outline.ShowOutline == true)
        //    Outline.enabled = false;

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
    private void Awake()
    {
        Image = GetComponent<Image>();
        OutlineSprite = shopItem.OutlineSprite;
        NoOutlineSprite = shopItem.Sprite;
        MeowSound = shopItem.MeowSound;
        button = GetComponent<Button>();
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
