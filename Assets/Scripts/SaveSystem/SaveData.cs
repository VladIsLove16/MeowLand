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
    public bool PromoEntered;
    public int Level;
    public SaveData() 
    { 
        Money = new Money();
        HealthData = new HealthData();
        ShopData = new ShopData();
        Level = 0;
        PromoEntered = false;
    }
    public SaveData(Money money, ShopData shopData, HealthData healthData, int level , bool enteredPromo)
    {
        Money = money;
        HealthData = healthData;
        ShopData = shopData;
        level = Level;
        PromoEntered= enteredPromo;
    } 
    public SaveData(Money money, ShopData shopData, HealthData healthData, int level)
    {
        Money = money;
        HealthData = healthData;
        ShopData = shopData;
        level = Level;
    }   
}
