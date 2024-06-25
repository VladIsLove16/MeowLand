using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
[Serializable]
public static class ShopData
{
    //public Dictionary<Guid, ShopItem> items=new();
    public static Dictionary<string, ShopDataItem> Data = new();

    public static void Set(string name, ShopDataItem item)
    {
        LoadIfNull();
        Data[name] = item;
        SaveChanges();
    }
    public static ShopDataItem Get(string name)
    {
        LoadIfNull();
        if(Data.TryGetValue(name, out ShopDataItem idata))
        return idata;
        else return null;
    }
    public static void LoadIfNull()
    {
        if (Data.Count == 0)
        {
            var LoadedShopData=new Dictionary<string, ShopDataItem>();
            try
            {
               LoadedShopData = SaveSystem.Load<Dictionary<string, ShopDataItem>>("ShopData");
                Debug.Write("Loaded");

            }
            catch
            {
                Debug.Write("notloaded");
                return;
            }
            Data= LoadedShopData;
        }
    }
    public static void SaveChanges()
    {
        SaveSystem.Save<Dictionary<string, ShopDataItem>>(Data, "ShopData");
    }
}
