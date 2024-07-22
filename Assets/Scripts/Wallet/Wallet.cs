using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Wallet",menuName = "new Wallet")]
public class Wallet : ScriptableObject
{
    public Money Money;
    public UnityEvent<Money> moneyChangedBy=new();
    public UnityEvent moneyChanged=new();
    private void Awake()
    {
        Debug.Log("Loading");
        Money money = SaveSystem.Load<Money>();
        if (money != null)
            Money = money;
        else
            Money = new Money(10, 0); 
    }
    public void AddMoney(Money money)
    {
        Money.SoftMoney += money.SoftMoney;
        Money.HardMoney += money.HardMoney;
        moneyChangedBy.Invoke(money);
        moneyChanged.Invoke();
        SaveSystem.Save(Money);
    }
    public bool SpendMoney(Money money)
    {
        if(!IsEnoughMoney(money))return false;  
        Money.SoftMoney -= money.SoftMoney;
        Money.HardMoney -= money.HardMoney;
        moneyChangedBy.Invoke(new Money(-money.SoftMoney,-money.HardMoney));
        moneyChanged.Invoke();
        SaveSystem.Save(Money);
        return true;
    }
    private bool IsEnoughMoney(Money money)
    {
        if (money.SoftMoney > Money.SoftMoney || money.HardMoney > Money.HardMoney) return false;
        return true;
    }
}
