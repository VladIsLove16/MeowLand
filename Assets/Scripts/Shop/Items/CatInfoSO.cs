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
    public Sprite FlashingSprite;
    public Money Cost;
    public bool IsUnlocked;
    public bool IsBought;
    public bool CanBeSold;
    //public string NotUnlockedMessage;
    //public string NotEnoughtMoneyMessage;
    public UnityEvent stateChanged;
    public void Awake()
    {
        //Debug.Log("So awake");
        //if(ShopData.Get(this,out ShopDataItem item))
        //{
        //    isBought = item.IsBought;
        //    isUnlocked = item.IsUnlocked;
        //};
        //stateChanged.AddListener(() =>
        //{
        //    ShopData.Set(this, new ShopDataItem() { IsBought = isBought, IsUnlocked = isUnlocked });
        //});
    }
    public virtual void Unlock()
    {
        IsUnlocked = true;
        stateChanged.Invoke();
    }
    public virtual void Buy()
    {
        IsBought = true;
        stateChanged.Invoke();
    }
    public virtual void Sell()
    {
        if (CanBeSold)
        {
            IsBought = false;
            stateChanged.Invoke();
        }
    }
    public virtual void Lock()
    {
        IsUnlocked = false;
        stateChanged.Invoke();
    }
}
