using Assets.Scripts;
using System;
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
    [HideInInspector]
    public CatAnimator CatAnimator;
    public CatInfoSO catInfoSO;
    [HideInInspector]
    private AudioSource AudioSource;
    [HideInInspector]

    public UnityEvent Clicked;
    private Button button;
    public void Init(CatInfoSO shopItemSO)
    {
        catInfoSO = shopItemSO;
        CatAnimator.Init(shopItemSO);
        gameObject.SetActive(true);
    }
    public void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        CatAnimator = GetComponent<CatAnimator>();
        button = GetComponent<Button>();
        SetAnimationMode(true);
        if(catInfoSO!=null)
            Init(catInfoSO);
    }
    public void SetAnimationMode(bool b)
    {
        CatAnimator.SetAnimationMode(b);
    }
    public void SetUnclickable(bool b)
    {
        button.enabled = b;
    }
    public void OnClick()
    {
        Debug.Log("ButtonClicked");
        Clicked.Invoke();
    }
    public void Meow()
    {
        AudioSource.PlayOneShot(catInfoSO.MeowSound);
        CatAnimator.StartAnimation(CatAnimator.AnimationType.Meow);
    }
    public void Angry()
    {
        if(catInfoSO.AngrySound!=null)
            AudioSource.PlayOneShot(catInfoSO.AngrySound);
        CatAnimator.StartAnimation(CatAnimator.AnimationType.Angry);
    }
    public void RandomEmodji()
    {

        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            Meow();
        }
        else
        {
            Angry();
        }
    }
}
