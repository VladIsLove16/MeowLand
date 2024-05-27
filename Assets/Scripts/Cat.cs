using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]
public class Cat : MonoBehaviour
{
    public int SceneID;
    public string Name;
    [SerializeField]
    public AudioClip MeowSound;
    [HideInInspector]
    public bool clickAvailable;
    private Button button;
    private AudioSource AudioSource;

    public bool OutlineEnabled;
    [SerializeField]
    private Sprite OutlineSprite;
    [SerializeField]
    private Sprite NoOutlineSprite;
    private SpriteRenderer spriteRenderer; 
    private Outline Outline;
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();   
        Outline = GetComponent<Outline>();
        SetOutlineMode(true);
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void Play()
    {
        AudioSource.PlayOneShot(MeowSound);
        ShowOutline();
    }
    public void SetOutlineMode(bool b)
    {
        OutlineEnabled = b;
        //if (Outline.ShowOutline == true)
        //    Outline.enabled = false;

    }
    private void OnButtonClick()
    {
        Debug.Log("ButtonClicked");
        if (clickAvailable)
        {
            Play();
        }
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
