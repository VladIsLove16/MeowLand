using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField]
    Wallet wallet;
    public void TryBuy(ShopItem item)
    {
       if( wallet.SpendMoney(item.Cost))
        {
            Buy(item);
        }
        else
        {
            CantBuy();
        }
    }

    private void Buy(ShopItem item)
    {
        Debug.Log("CAT is in ur Inventory");
    }

    private void CantBuy()
    {
        Debug.Log("CANT BUY");
    }
}
