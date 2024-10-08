﻿using System.Collections.Generic;
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
            if (cat != null)
                cat.gameObject.SetActive(false);
        }
        for (int i = 0; i < StartedAvailableCatsCount; i++)
            AddNewCatInGame();
        SoundSequenceGame.SetUpGame(EnabledCats, StartedSequenceLength);
    }

    private Cat GetDisabledCat()
    {
        return GetRandom(DisabledCats);
    }

    public void AddNewCatInGame()
    {
        if (!DisabledCats.Any())
            return;

        Cat cat = GetDisabledCat();
        if (cat == null) return;

        EnabledCats.Add(cat);
        DisabledCats.Remove(cat);
        cat.gameObject.SetActive(true);
        CatInfoSO catInfoSO = AddNewCatInfoInPlay();
        if(catInfoSO != null)
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
        if (HealthSystem.Health == 0)
        {
            Debug.Log("Health is 0");
            return;
        }
        Invoke(nameof(SoundSequenceGame.StartRound), 1f);
    }

    private void OnRoundWon(int round)
    {
        if(HealthSystem.Health>0)
            wallet.AddMoney(new Money() { SoftMoney = round});
        if (RoundToAddNewAvailableCat == 0)
            return;

        if (round % RoundToAddNewAvailableCat == 0)
        {
            Cat randomCat = GetRandom(DisabledCats);
            if (randomCat == null) return;

            randomCat.Init(AddNewCatInfoInPlay());
            randomCat.gameObject.SetActive(true);
            SoundSequenceGame.AddNewCat(randomCat);
        }
    }
}
