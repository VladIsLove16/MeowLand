using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
[Serializable]
public class ShopData
{
    //public Dictionary<Guid, ShopItem> items=new();
    public Dictionary<string, ShopDataItem> Data = new();

    public void Set(string name, ShopDataItem item)
    {
        LoadIfNull();
        Data[name] = item;
        SaveChanges();
    }
    public ShopDataItem Get(string name)
    {
        LoadIfNull();
        Data.TryGetValue(name, out ShopDataItem idata);
        return idata;
    }
    private void LoadIfNull()
    {
        if (Data.Count == 0)
        {
            ShopData LoadedShopData;
            try
            {
                LoadedShopData = SaveSystem.Load<ShopData>();
            }
            catch
            {
                LoadedShopData = null;
            }
            if (LoadedShopData == null)
                return;
            Data= LoadedShopData.Data;
        }
    }
    public void SaveChanges()
    {
        SaveSystem.Save<ShopData>(this);
    }
}
