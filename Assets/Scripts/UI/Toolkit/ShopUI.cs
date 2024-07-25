using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.UI.Toolkit
{
    public class ShopUI : MonoBehaviour
    {
        private UIDocument document;
        private VisualElement root;
        [SerializeField]
        Wallet  wallet;
        Label SoftMoney;
        GroupBox CatRow1;
        GroupBox CatRow2;
        [SerializeField]   
        Shop shop;
        [SerializeField]
        List<Cat> cats;
        [SerializeField]
        Sprite AlreadyBought;
        Label notenoughmoneyText;
        Button Close;
        
        private void Awake()
        {
            document = GetComponent<UIDocument>();
            root = document.rootVisualElement;
            SoftMoney = root.Q("Money").Q("MoneyText") as Label;
            CatRow1 = root.Q("BuyOrActivate") as GroupBox;
            CatRow2 = root.Q("BuyOrActivate1") as GroupBox;
            Close = root.Q("Close") as Button;
            Close.clicked += () => Loader.Load(Loader.Scene.MainMenu);
            int i = 0;
            SetupCatShopItems(CatRow1.Children(),ref i);
            SetupCatShopItems(CatRow2.Children(),ref i);

            //HardMoney.text=wallet.Money.HardMoney.ToString();
            wallet.moneyChanged.AddListener(OnMoneyChange);

            notenoughmoneyText = root.Q("notenoughmoneyText") as Label;
            notenoughmoneyText.SetEnabled(false);

        }
        private void Start()
        {
            SoftMoney.text = wallet.Money.SoftMoney.ToString();
            notenoughmoneyText.SetEnabled(false);
        }
        private void SetupCatShopItems(IEnumerable<VisualElement> children, ref int j)
        {
            foreach (VisualElement item in children)
            {
                VisualElement root = item.Q("CatShopItemUI");
                VisualElement buttons = root.Q("Buttons");
                
                Button buy = buttons.Q("Buy") as Button;
                int currentIndex = j;
                if (cats[currentIndex].shopItem.IsBought)
                {
                    buy.SetEnabled(false); 
                    buy.text = "Куплено"; 
                }
                else
                    buy.text += " за " + cats[currentIndex].shopItem.Cost.SoftMoney;

                buy.clicked += () => {TryBuy(currentIndex, buy); };
                Button activate = buttons.Q("Activate") as Button;
                j++;
            }
        }
        private void TryBuy(int j,Button buy)
        {
            if (shop.TryBuy(cats[j].shopItem))
            {
                buy.SetEnabled(false);
                buy.text = "Куплено";
            }
            else
            {
                CantBuy();
            }
        }

        private async Task CantBuy()
        {
            notenoughmoneyText.SetEnabled(true);
            await Task.Delay(1500);
            notenoughmoneyText.SetEnabled(false);
        }

        private void OnMoneyChange()
        {
            //HardMoney.text = money.HardMoney.ToString();
            SoftMoney.text = wallet.Money.SoftMoney.ToString();
        }
    }
    class BuyButton
    {

    }
}
