using Assets.Scripts.UI.Toolkit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class SequenceGameManager : MonoBehaviour
{
    [SerializeField]
    SoundSequenceGame SoundSequenceGame;
    [SerializeField]
    Wallet wallet;
    [HideInInspector]
    public UnityEvent<int> HealthChanged;
    [SerializeField]
    public List<CatInfoSO> AllCatsInfo;
    [HideInInspector]
    public List<CatInfoSO> WaitingTheirTimeCatsInfo = new();
    [HideInInspector]
    public List<CatInfoSO> CatsInfoInPlay = new();
    [SerializeField]
    public List<Cat> DisabledCats;
    [SerializeField]
    public List<Cat> EnabledCats;
    public int StartedSequenceLength;
    public int StartedAvailableCatsCount;
    public int RoundToAddNewAvailableCat;
    public GameUi GameUi;
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
    public List<Cat> GetCats()
    {
        List<Cat> catList = new List<Cat>(EnabledCats);
        foreach (Cat cat in DisabledCats)
            catList.Add(cat);
        return catList;
    }
    private void GameInit()
    {
        List<Cat> cats = GetCats();
        while (cats.Any())
        {
            DisableCat(cats[0]);
            cats.RemoveAt(0);
        }
        for (int i = 0; i < StartedAvailableCatsCount; i++)
            AddNewCatInGame();
        SoundSequenceGame.SetUpGame(EnabledCats, StartedSequenceLength);
    }
    private void DisableCat(Cat cat)
    {
        cat.Disable();
        if (EnabledCats.Contains(cat))
            EnabledCats.Remove(cat);
        if (!DisabledCats.Contains(cat))
            DisabledCats.Add(cat);
    }
    public void EnableCat(Cat cat)
    {
        cat.Enable();
        if (!EnabledCats.Contains(cat))
            EnabledCats.Add(cat);
        if (DisabledCats.Contains(cat))
            DisabledCats.Remove(cat);
    }

    private Cat GetDisabledCat()
    {
        return GetRandom(DisabledCats);
    }
    public void OnCatClick(Cat cat)
    {
        if (cat.IsClickable)
        {
            SoundSequenceGame.OnCat_Click(cat);
        }
    }
    public void AddNewCatInGame()
    {
        if (!DisabledCats.Any())
            return;
        AddNewCatInGame(AddNewCatInfoInPlay());

    }
    private void AddNewCatInGame(CatInfoSO catInfoSO)
    {
        Cat cat = GetDisabledCat();
        if (cat == null)
            return;

        EnableCat(cat);
        cat.Init(catInfoSO);
    }
    private CatInfoSO AddNewCatInfoInPlay()
    {
        if (WaitingTheirTimeCatsInfo.Count == 0)
            WaitingTheirTimeCatsInfo = GetBoughtCats();
        CatInfoSO randomCat = GetRandom(WaitingTheirTimeCatsInfo);
        if (randomCat == default || randomCat == null)
            return null;
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
                BoughtCats.Add(item);
        }
        return BoughtCats;
    }

    private static T GetRandom<T>(List<T> list)
    {
        if (list == null || list.Count == 0) return default(T);
        int num = Random.Range(0, list.Count);
        return list[num];
    }

    public void HealAndSaveRound(int health)
    {
        wallet.SpendMoney(new Money(0, 1));
        SoundSequenceGame.StartNewGame();
    }

    private void OnRoundLost(int round)
    {
        //wallet.SpendMoney(new Money() { SoftMoney = 1 });
        HealthSystem.LoseHealth(1);
        YGAdsProvider.TryShowFullscreenAdWithChance(round*5);
        //Invoke(nameof(SoundSequenceGame.StartRound), 1f);
    }

    private void OnRoundWon(int round)
    {
        if (HealthSystem.Health > 0)
            wallet.AddMoney(new Money(round));
        if (RoundToAddNewAvailableCat == 0)
            return;
        else if (round % RoundToAddNewAvailableCat == 0)
        {

            AddNewCatInGame();
        }
    }

}
