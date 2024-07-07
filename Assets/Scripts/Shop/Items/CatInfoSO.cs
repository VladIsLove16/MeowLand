using System;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "new Item", menuName = "ShopItems/Item")]
public class CatInfoSO : ScriptableObject
{
    public AudioClip MeowSound;
    public AudioClip AngrySound;
    public AnimationClip IdleClip;
    public AnimationClip AngryClip;
    public AnimationClip MeowClip;
    public Sprite IdleSprite;
    public Sprite MeowSprite;
    public Sprite AngrySprite;
    public Money Cost;
    [SerializeField]
    private bool isUnlocked;
    // Свойство позволяет только чтение из кода
    public bool IsUnlocked
    {
        get { return isUnlocked; }
    }
    [SerializeField]
    private bool isBought;

    // Свойство позволяет только чтение из кода
    public bool IsBought
    {
        get { return isBought; }
    }
    public bool CanBeSold;
    //public string NotUnlockedMessage;
    //public string NotEnoughtMoneyMessage;
    public UnityEvent stateChanged;
    public void Awake()
    {
        Debug.Log("So awake");
        if(ShopData.Get(this,out ShopDataItem item))
        {
            isBought = item.IsBought;
            isUnlocked = item.IsUnlocked;
        }; 
    }
    public virtual void Unlock()
    {
        isUnlocked = true;
        stateChanged.Invoke();
    }
    public virtual void Buy()
    {
        isBought = true;
        stateChanged.Invoke();
    }
    public virtual void Sell()
    {
        if (CanBeSold)
        {
            isBought = false;
            stateChanged.Invoke();
        }
    }
    public virtual void Lock()
    {
        isUnlocked = false;
        stateChanged.Invoke();
    }
}
