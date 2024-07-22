using System;

[Serializable]
public class ShopDataItem
{
    public bool IsBought;
    public bool IsUnlocked;
    public ShopDataItem() { }   
    public ShopDataItem(CatInfoSO cat)
    {
        IsUnlocked = cat.IsUnlocked;
        IsBought = cat.IsBought;
    }
}
