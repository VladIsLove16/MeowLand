using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SequenceGameManager : MonoBehaviour
{
    [SerializeField]
    SoundSequenceGame SoundSequenceGame;
    [SerializeField]
    Wallet wallet;
    [HideInInspector]
    public UnityEvent<int> HealthCHanged;
    [SerializeField]
    public List<CatInfoSO> AllCatsInfo;
    [HideInInspector]
    public List<CatInfoSO> WaitingTheirTimeCatsInfo = new();
    [HideInInspector]
    public List<CatInfoSO> CatsInfoInPlay=new();
    [SerializeField]
    public List<Cat> DisabledCats;
    [SerializeField]
    public List<Cat> EnabledCats;
    public int StartedSequenceLength;
    public int StartedAvailableCatsCount;
    public int RoundToAddNewAvailableCat;
    private void Awake()
    {
        SoundSequenceGame.Awake();
        SoundSequenceGame.RoundWon.AddListener(OnRoundWon);
        SoundSequenceGame.RoundLost.AddListener(OnRoundLost);
    }
    private void Start()
    {
        GameInit();
    }
    private void GameInit()
    {
        foreach (var cat in DisabledCats)
        {
            cat.gameObject.SetActive(false);
        }
        for (int i = 0; i < StartedAvailableCatsCount; i++)
            AddNewCatInGame();
        SoundSequenceGame.SetUpGame(EnabledCats, StartedSequenceLength);
        //SoundSequenceGame.StartNewGame();
    }
    private Cat GetDisabledCat()
    {
        return DisabledCats[0];
    }
    public void AddNewCatInGame()
    {
        if (!DisabledCats.Any()) 
            return;
        Cat cat = GetDisabledCat();//GetRandom(DisabledCats);
        EnabledCats.Add(cat);
        DisabledCats.Remove(cat);
        cat.gameObject.SetActive(true);
        cat.Init(AddNewCatInfoInPlay());
    }
    private CatInfoSO AddNewCatInfoInPlay()
    {
        if (WaitingTheirTimeCatsInfo.Count == 0)
            WaitingTheirTimeCatsInfo = GetBoughtCats();
        CatInfoSO randomCat = GetRandom(WaitingTheirTimeCatsInfo);
        CatsInfoInPlay.Add(randomCat);
        WaitingTheirTimeCatsInfo.Remove(randomCat);
        return randomCat;
    }
    private void SetCatsSOList()
    {
        for (int i = 0; i < StartedAvailableCatsCount; i++)
            AddNewCatInfoInPlay();
    }

    private List<CatInfoSO> GetBoughtCats()
    {
        List<CatInfoSO> BoughtCats = new();
        foreach (var item in AllCatsInfo)
        {
            if (item.IsBought)
            { BoughtCats.Add(item); }
        }
        return BoughtCats;
    }
    private static T GetRandom<T>(List<T> list)
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
        if (RoundToAddNewAvailableCat == 0) 
            return; 
        if  (round % RoundToAddNewAvailableCat == 0)
        {
            Cat randomCat = DisabledCats[StartedSequenceLength++];
            randomCat.Init(AddNewCatInfoInPlay());
            randomCat.gameObject.SetActive(true);
            SoundSequenceGame.AddNewCat(randomCat);
        }
    }
}
