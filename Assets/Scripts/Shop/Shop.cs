using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    Wallet wallet;
    [SerializeField]
    List<ShopItemView> DisplayShopItemList;
    ShopData data = new();
    public void Awake()
    {
        foreach (var DisplayshopItem in DisplayShopItemList)
        {
            ShopDataItem ShopItemData = data.Get(DisplayshopItem.item.name);
            if (ShopItemData != null)
            {
                DisplayshopItem.item.IsBought = ShopItemData.IsBought;
                DisplayshopItem.item.IsUnlocked = ShopItemData.IsUnlocked;
            }
            DisplayshopItem.item.stateChanged.AddListener(() => SaveChanges(DisplayshopItem.item.name, DisplayshopItem.item.IsBought, DisplayshopItem.item.IsUnlocked));
        }
    }
    private void SaveChanges(string item,bool a ,bool b)
    {
        Debug.Log("SaveChanges called");
            data.Set(item, new ShopDataItem() { IsBought = a, IsUnlocked = b });
    }
    public void TryBuy(ShopItem item)
    {
        if(item.IsUnlocked==false)
        {
            AlreadyBought(item);
            return;
        }
        if (item.IsBought == true   )
        {
            AlreadyBought(item);
            return;
        }
        if (wallet.SpendMoney(item.Cost))
        {
            Buy(item);
        }
        else
        {
            CantBuy(item);
        }
    }
    public void SellAll()
    {
        foreach (var DisplayshopItem in DisplayShopItemList)
        {
            DisplayshopItem.item.Sell();
        }
    }
    public void Unlock(ShopItem item)
    {
        Debug.Log("Unlocked");
        item.Unlock();
    }
    private void AlreadyBought(ShopItem item)
    {
        Debug.Log("AlreadyBought");
    }
    
    private void Buy(ShopItem item)
    {
        Debug.Log("Bought");
        item.Buy();
    }

    private void CantBuy(ShopItem item)
    {
        
        if(wallet.Money.SoftMoney<item.Cost.SoftMoney)
            Debug.Log("not enough softMoney");
        if(wallet.Money.HardMoney<item.Cost.HardMoney)
            Debug.Log("not enough HardMoney");
    }
    public void ReloadScene()
    {
        Loader.Load(Loader.Scene.Shop);
    }
}
