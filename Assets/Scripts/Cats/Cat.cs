using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(UIToolkitButtonAnimator))]
public class Cat : MonoBehaviour
{
    [HideInInspector]
    public UIToolkitButtonAnimator CatAnimator;
    public CatInfoSO catInfoSO;
    [HideInInspector]
    private AudioSource AudioSource;
    public bool IsClickable;
    public void Init(CatInfoSO shopItemSO)
    {
        catInfoSO = shopItemSO;
        CatAnimator.Init(shopItemSO);
        gameObject.SetActive(true);
    }
    public void Init(Button btn)
    {
        CatAnimator.Init(btn);
    }
        //public void Init(UIToolkitButtonAnimator catAnimator)
        //{
        //    CatAnimator = catAnimator;
        //}
    public void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        CatAnimator = GetComponent<UIToolkitButtonAnimator>();
        if (catInfoSO!=null)
            Init(catInfoSO);
    }
    private void Update()
    {
        CatAnimator.Update();
    }
    public void Disable()
    {
        gameObject.SetActive(false);
    }
    public void Enable()
    {
        gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        CatAnimator.OnEnable();
    }
    public void OnDisable()
    {
        CatAnimator.OnDisable();
    }
    public void Meow()
    {
        if (IsClickable)
        {
            AudioSource.PlayOneShot(catInfoSO.MeowSound);
            CatAnimator.StartAnimation(AnimationType.Meow);
        }
    }
    public void Angry()
    {
        if (IsClickable)
        {
            if (catInfoSO.AngrySound != null)
                AudioSource.PlayOneShot(catInfoSO.AngrySound);
            CatAnimator.StartAnimation(AnimationType.Angry);
        }
    }
    public void RandomEmodji()
    {
        if (IsClickable)
            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                Meow();
            }
            else
            {
                Angry();
            }
    }

    internal void SetUnclickable(bool b)
    {
        IsClickable = b;
    }
}
