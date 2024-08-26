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
    public int Level;
    public SaveData() 
    { 
        Money = new Money();
        HealthData = new HealthData();
        ShopData = new ShopData();
        Level = 0;
    }
    public SaveData(Money money, ShopData shopData, HealthData healthData, int level = 0)
    {
        Money = money;
        HealthData = healthData;
        ShopData = shopData;
        level = Level;
    }   
}
