using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[Serializable]
public class ShopData
{
    private Dictionary<string, ShopDataItem> Data = new();
    public void Set(CatInfoSO infoSO, ShopDataItem item)
    {
        Data[infoSO.name] = item;
    }
    public bool Get(CatInfoSO infoSO, out ShopDataItem item)
    {
        return Data.TryGetValue(infoSO.name, out item);
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
