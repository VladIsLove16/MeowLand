using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Wallet : ScriptableObject
{
    public Money Money;
    [System.NonSerialized]
    public UnityEvent<Money> moneyChanged;
    public void AddMoney(Money money)
    {
        Money.SoftMoney += money.SoftMoney;
        Money.HardMoney += money.HardMoney;
        moneyChanged.Invoke(money);
    }
    public bool SpendMoney(Money money)
    {
        if(money.SoftMoney<Money.SoftMoney || money.HardMoney < Money.HardMoney)return false;
        Money.SoftMoney -= money.SoftMoney;
        Money.HardMoney -= money.HardMoney;
        moneyChanged.Invoke(money);
        return true;
    }
}
