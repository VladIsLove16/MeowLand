using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(fileName = "Wallet",menuName = "new Wallet")]
public class Wallet : ScriptableObject
{
    public Money Money;
    public UnityEvent<Money> moneyChanged=new();
    private void Awake()
    {
        Debug.Log("Loading");
        Money wallet = SaveSystem.Load<Money> ();
        Money = wallet;
    }
    public void AddMoney(Money money)
    {
        Money.SoftMoney += money.SoftMoney;
        Money.HardMoney += money.HardMoney;
        moneyChanged.Invoke(Money);
        SaveSystem.Save(Money);
    }
    public bool SpendMoney(Money money)
    {
        if(!IsEnoughMoney(money))return false;  
        Money.SoftMoney -= money.SoftMoney;
        Money.HardMoney -= money.HardMoney;
        moneyChanged.Invoke(Money);
        SaveSystem.Save(Money);
        return true;
    }
    private bool IsEnoughMoney(Money money)
    {
        if (money.SoftMoney > Money.SoftMoney || money.HardMoney > Money.HardMoney) return false;
        return true;
    }
}
