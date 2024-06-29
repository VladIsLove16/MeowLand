using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[Serializable]
public class ShopData : MonoBehaviour
{
    //public Dictionary<Guid, ShopItem> items=new();
    private static Dictionary<string, ShopDataItem> Data=new();
    public static void Set(CatInfoSO infoSO, ShopDataItem item)
    {
        LoadIfNull();
        Data[infoSO.name] = item;
        SaveChanges();
    }
    public static bool Get(CatInfoSO infoSO, out ShopDataItem item)
    {
        LoadIfNull();
        return Data.
            TryGetValue(infoSO.name, out item);
        
    }
    public static void LoadIfNull()
    {
        if (Data.Count == 0)
        {
            Dictionary<string, ShopDataItem> LoadedShopData;
            try
            {
                LoadedShopData = SaveSystem.Load<Dictionary<string, ShopDataItem>>("ShopData");
                Debug.Log("Loaded");

            }
            catch(Exception e)
            {
                Debug.Log("notloaded");
                Debug.Log(e);
                return;
            }
            if(LoadedShopData != null)
                Data= LoadedShopData;
        }
    }
    public static void SaveChanges()
    {
        SaveSystem.Save<Dictionary<string, ShopDataItem>>(Data, "ShopData");
    }
}
