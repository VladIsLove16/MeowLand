using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Toolkit
{
    public class ShopUI : MonoBehaviour
    {
        [SerializeField]
        Wallet  wallet; 
        [SerializeField]
        TextMeshProUGUI SoftMoney; 
        [SerializeField]
        TextMeshProUGUI HardMoney;
        private void Awake()
        {
            HardMoney.text=wallet.Money.HardMoney.ToString();
            SoftMoney.text=wallet.Money.SoftMoney.ToString();
            wallet.moneyChanged.AddListener(OnMoneyChange);
        }
        private void OnMoneyChange(Money money)
        {
            HardMoney.text = money.HardMoney.ToString();
            SoftMoney.text = money.SoftMoney.ToString();
        }
    }
}
