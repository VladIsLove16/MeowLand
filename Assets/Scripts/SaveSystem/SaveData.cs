using UnityEngine;
using System.Collections;
using YG;
using System.Collections.Generic;
using System;
[Serializable]
public class SaveData
{
    public Money Money;
    public HealthData HealthData;
    public ShopData ShopData;
    public SaveData() 
    { 
        Money = new Money();
        HealthData = new HealthData();
        ShopData = new ShopData();
    }
    public SaveData(Money money, ShopData shopData, HealthData healthData)
    {
        Money = money;
        HealthData = healthData;
        ShopData = shopData;
    }   
}
