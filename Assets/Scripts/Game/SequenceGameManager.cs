using UnityEngine;
using UnityEngine.Events;

public class SequenceGameManager : MonoBehaviour
{
    [SerializeField]
    Wallet wallet;
    [SerializeField]
    public int Health;
    public UnityEvent<int> HealthCHanged;
    private void Awake()
    {
        SoundSequenceController.instance.RoundWon.AddListener(OnRoundWon);
        SoundSequenceController.instance.RoundLost.AddListener(OnRoundLost);
    }
    public void HealAndSaveRound(int health)
    {
        wallet.SpendMoney(new Money(0, 1));
        Health = health;
        HealthCHanged.Invoke(Health);
        SoundSequenceController.instance.StartNewGame();
    }

    private void OnRoundLost(int round)
    { 
        wallet.SpendMoney(new Money() { SoftMoney = 1 });
        Health--;
        HealthCHanged.Invoke(Health);
        if (Health==0)
        {
            Debug.Log("Health is 0");
            return;
        }
        SoundSequenceController.instance.StartRound();
    }
    private void OnRoundWon(int round)
    {
        wallet.AddMoney(new Money() { SoftMoney = round - 1 });
        //SoundSequenceController.instance.AddNewCat(new Cat());
    }
}
