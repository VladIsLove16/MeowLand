using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "new Item", menuName = "ShopItems/ShopDataSaver")]
public class ShopDataSaver : ScriptableObject
{
    public List<CatInfoSO> Cats;
    public void Load(ShopData data)
    {
        foreach (CatInfoSO cat in Cats)
        { 
            data.Get(cat, out ShopDataItem item);
            if (item != null)
            {
                cat.IsBought = item.IsBought;
                cat.IsUnlocked = item.IsUnlocked;
            }
        }
    }
    public ShopData Get()
    {
        ShopData data = new ShopData();
        foreach (CatInfoSO cat in Cats)
        {
            data.Set(cat, new ShopDataItem(cat));
        }
        return data;
    }
    //public static void LoadIfNull()
    //{
    //    if (Data.Count == 0)
    //    {
    //        Dictionary<string, ShopDataItem> LoadedShopData;
    //        try
    //        {
    //            LoadedShopData = JsonSaveSystem.Load<Dictionary<string, ShopDataItem>>("ShopData");
    //            Debug.Log("Loaded");

    //        }
    //        catch(Exception e)
    //        {
    //            Debug.Log("notloaded");
    //            Debug.Log(e);
    //            return;
    //        }
    //        if(LoadedShopData != null)
    //            Data= LoadedShopData;
    //    }
    //}
}
