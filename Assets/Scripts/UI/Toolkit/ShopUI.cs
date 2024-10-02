﻿using System;
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
        Wallet  Wallet;
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
            CatRow2 = root.Q("BuyOrActivate2") as GroupBox;
            Close = root.Q("Close") as Button;
            Close.clicked += () => Loader.Load(Loader.Scene.MainMenu);
            int i = 0;
            SetupCatShopItems(CatRow1.Children(),ref i);
            SetupCatShopItems(CatRow2.Children(),ref i);

            //HardMoney.text=wallet.Money.HardMoney.ToString();
            Wallet.moneyChanged.AddListener(OnMoneyChange);

            notenoughmoneyText = root.Q("notenoughmoneyText") as Label;
            notenoughmoneyText.SetEnabled(false);
            Debug.Log("ShopUI Loaded with "+ Wallet.Money.SoftMoney.ToString() + "soft money");
        }
        private void Start()
        {
            SoftMoney.text = Wallet.Money.SoftMoney.ToString();
            notenoughmoneyText.SetEnabled(false);
        }
        private void SetupCatShopItems(IEnumerable<VisualElement> children, ref int j)
        {
            foreach (VisualElement item in children)
            {
                VisualElement root = item.Q("CatShopItemUI2_0");
                Button buy = root.Q("Buy") as Button;
                Button catBtn = root.Q("Cat") as Button;
                int currentIndex = j;
                Cat cat = cats[currentIndex];
                CatInfoSO catInfo = cat.catInfoSO;
                cat.Init(catInfo);
                if (catInfo.IsBought)
                {
                    buy.SetEnabled(false); 
                    buy.text = "Куплено"; 
                }
                else
                    buy.text = "Купить за " + catInfo.Cost.SoftMoney;
                buy.clicked += () => {TryBuy(currentIndex, buy); };
                catBtn.clicked += () => { cat.RandomEmodji(); };
                //Button activate = buttons.Q("Activate") as Button;
                j++;
            }
        }
        private void TryBuy(int j,Button buy)
        {
            if (shop.TryBuy(cats[j].catInfoSO))
            {
                buy.SetEnabled(false);
                buy.text = "Куплено";
            }
            else
            {
               StartCoroutine(CantBuy());
            }
        }

        private IEnumerator CantBuy()
        {
            notenoughmoneyText.SetEnabled(true);
            yield return new WaitForSeconds(1.5f);
            notenoughmoneyText.SetEnabled(false);
        }

        private void OnMoneyChange()
        {
            //HardMoney.text = money.HardMoney.ToString();
            SoftMoney.text = Wallet.Money.SoftMoney.ToString();
        }
    }
    class BuyButton
    {

    }
}
