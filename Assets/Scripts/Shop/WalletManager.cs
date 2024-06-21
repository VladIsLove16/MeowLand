using UnityEngine;
public class WalletManager : MonoBehaviour
{
    public static WalletManager instance;
    public Wallet wallet;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        SoundSequenceController.instance.RoundWon.AddListener(OnROundWon);
        SoundSequenceController.instance.RoundLost.AddListener(OnROundWon);
    }
    private void OnROundWon(int round)
    {
        wallet.AddMoney(new Money() { SoftMoney = round + 1 });
    }
    private void OnROundLost(int round)
    {
        wallet.SpendMoney(new Money() { SoftMoney = 1 });
    }
}
