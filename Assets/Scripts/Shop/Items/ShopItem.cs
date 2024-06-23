using System;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "new Item", menuName = "ShopItems/Item")]
public class ShopItem : ScriptableObject
{
    public Sprite Sprite;
    public Sprite OutlineSprite;
    public AudioClip MeowSound;
    public Money Cost;
    public bool IsUnlocked;
    public bool IsBought;
    public string NotUnlockedMessage;
    public string NotEnoughtMoneyMessage;
    public UnityEvent stateChanged;
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
        IsBought = false;
        stateChanged.Invoke();
    }
    public virtual void Lock()
    {
        IsUnlocked = false;
        stateChanged.Invoke();
    }
}
