using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceGameManager : MonoBehaviour
{
    [SerializeField]
    SoundSequenceGame SoundSequenceGame;
    [SerializeField]
    Wallet wallet;
    public UnityEvent<int> HealthCHanged;
    [SerializeField]
    public List<Cat> AllTheCatsWeKnow;
    public List<Cat> CatsInPlay=new();
    public List<Cat> WaitingTheirTimeCats=new();
    public int StartedSequenceLength;
    public int StartedAvailableCatsCount;
    public int RoundToAddNewCat;
    private void Awake()
    {
        SoundSequenceGame.Awake();
        SoundSequenceGame.RoundWon.AddListener(OnRoundWon);
        SoundSequenceGame.RoundLost.AddListener(OnRoundLost);
        WaitingTheirTimeCats = GetBoughtCats();
        for (int i = 0; i < StartedAvailableCatsCount; i++)
        {
            Cat randomCat = GetRandom(WaitingTheirTimeCats);
            CatsInPlay.Add(randomCat);
            WaitingTheirTimeCats.Remove(randomCat);
        }
        foreach (Cat cat in WaitingTheirTimeCats)
        {
            cat.gameObject.SetActive(false);
        }
        SoundSequenceGame.SetUpGame(CatsInPlay, 2);
        //SoundSequenceGame.StartNewGame();
    }
    private List<Cat> GetBoughtCats()
    {
        List<Cat> BoughtCats = new();
        foreach (var cat in AllTheCatsWeKnow)
        {
            if (cat.shopItem.IsBought)
            { BoughtCats.Add(cat); }
        }
        return BoughtCats;
    }
    private T GetRandom<T>(List<T> list)
     {
        if (list.Count == 0) return default(T);
        int count  = list.Count;
        int num = Random.Range(0, count);
        return list[num];
     }

    public void HealAndSaveRound(int health)
    {
        wallet.SpendMoney(new Money(0, 1));
        SoundSequenceGame.StartNewGame();
    }

    private void OnRoundLost(int round)
    { 
        wallet.SpendMoney(new Money() { SoftMoney = 1 });
        HealthSystem.LoseHealth(1);
        if(HealthSystem.Health == 0)
        {
            Debug.Log("Health is 0");
            return;
        }
        Invoke("SoundSequenceGame.StartRound", 1f);
        //SoundSequenceGame.StartRound();
    }
    private void OnRoundWon(int round)
    {
        wallet.AddMoney(new Money() { SoftMoney = round - 1 });
        if (RoundToAddNewCat == 0) 
            return; 
        if  (round % RoundToAddNewCat == 0)
        {
            Cat randomCat = GetRandom(WaitingTheirTimeCats);
            CatsInPlay.Add(randomCat);
            WaitingTheirTimeCats.Remove(randomCat);
            SoundSequenceGame.AddNewCat(randomCat);
            randomCat.gameObject.SetActive(true);
        }
    }
}
