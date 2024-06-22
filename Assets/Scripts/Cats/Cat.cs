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
    public string Name;
    [SerializeField]
    public AudioClip MeowSound;
    [HideInInspector]
    public bool clickAvailable;
    private AudioSource AudioSource;
    public UnityEvent<Cat> OnPlay;
    public bool OutlineEnabled;
    [SerializeField]
    private Sprite OutlineSprite;
    [SerializeField]
    private Sprite NoOutlineSprite;
    private SpriteRenderer spriteRenderer;
    public void SetOutlineMode(bool b)
    {
        OutlineEnabled = b;
        //if (Outline.ShowOutline == true)
        //    Outline.enabled = false;

    }
    public void Click()
    {
        Debug.Log("ButtonClicked");
        if (clickAvailable)
        {
            Play();
        }
    }
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();   
        SetOutlineMode(true);
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Play()
    {
        
        OnPlay.Invoke(this);
        AudioSource.PlayOneShot(MeowSound);
        ShowOutline();
    }
   
    private void ShowOutline() 
    {
        if(Outlinev2.state==Outlinev2.OutlineState.show)
        { //if (Outline.ShowOutline == true)
            //    Outline.enabled = true;
            spriteRenderer.sprite = OutlineSprite;
            Invoke("HideOutline", MeowSound.length);
        }
    }
    private void HideOutline()
    {
        spriteRenderer.sprite = NoOutlineSprite;
    }
}
