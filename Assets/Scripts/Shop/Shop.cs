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
    public void Awake()
    {
        foreach (var DisplayshopItem in DisplayShopItemList)
        {
            CatInfoSO item = DisplayshopItem.cat.shopItem;
            DisplayshopItem.OnClick.AddListener(() => TryBuy(item));
            item.stateChanged.AddListener(() => SaveChanges(item, item.IsBought, item.IsUnlocked));
        }
    }
    private void SaveChanges(CatInfoSO item,bool a ,bool b)
    {
        Debug.Log("SaveChanges called");
        ShopData.Set(item, new ShopDataItem() { IsBought = a, IsUnlocked = b });
    }
    public void TryBuy(CatInfoSO item)
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
            DisplayshopItem.cat.shopItem.Sell();
        }
    }
    public void Unlock(CatInfoSO item)
    {
        Debug.Log("Unlocked");
        item.Unlock();
    }
    private void AlreadyBought(CatInfoSO item)
    {
        Debug.Log("AlreadyBought");
    }
    
    private void Buy(CatInfoSO item)
    {
        Debug.Log("Bought");
        item.Buy();
    }

    private void CantBuy(CatInfoSO item)
    {
        
        if(wallet.Money.SoftMoney<item.Cost.SoftMoney)
            Debug.Log("not enough softMoney");
        if(wallet.Money.HardMoney<item.Cost.HardMoney)
            Debug.Log("not enough HardMoney");
    }
    public void ReloadScene()
    {
        Loader.Load(Loader.Scene.Shop);
    }public void GoToMainScene()
    {
        Loader.Load(Loader.Scene.MainMenu);
    }
}
