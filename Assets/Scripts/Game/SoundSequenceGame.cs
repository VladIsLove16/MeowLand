using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;
using static Assets.Scripts.Outlinev2;

public class SoundSequenceGame : MonoBehaviour
{
    public static SoundSequenceGame instance;
    [SerializeField]
    public int StartedSequenceLength;
    public List<Cat> AvailableCats;
    public List<Cat> CatSequence;
    [HideInInspector]
    private AudioSource audioSource;
    public AudioClip loseSound;
    public AudioClip winSound;
    public bool PlayFullWinSound;
    public bool catsClickable;
    public UnityEvent<RoundState> roundStateChanged;
    public UnityEvent<int> RoundWon;
    public UnityEvent<int> RoundLost;
    public int CurrentNum;
    [SerializeField]
    public RoundState roundState { get; private set; } = RoundState.gameStarting;
    public enum RoundState
    {
        gameStarting,
        playingSounds,
        roundStarting,
        playing,
        lost,
    }
    public void Awake()
    {
        instance=this;
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        foreach (Cat cat in AvailableCats)
        {
            cat.Clicked.RemoveAllListeners();
            cat.Clicked.AddListener(() => OnCat_Click(cat));
        }
    }
    public void SetUpGame(List<Cat> AvailableCats, int StartedSequenceLength)
    {
        this.AvailableCats = AvailableCats;
        this.StartedSequenceLength = StartedSequenceLength;
    }
    public void StartNewGame()
    {
        
        CreateSequence(StartedSequenceLength);
        StartRound();
        YandexGame.GameReadyAPI();
    }
    public void StartRound()
    {
        SetRoundState(RoundState.roundStarting);
        CurrentNum = 0;
        StartCoroutine(PlayStartSoundSequence());
    }

    private void SetRoundState(RoundState state)
    {
        roundState = state;
        roundStateChanged.Invoke(roundState);
    }

    private IEnumerator PlayStartSoundSequence()
    {
        SetRoundState(RoundState.playingSounds);
        SetCatsClickable(false);
        for (int i = 0; i < CatSequence.Count; i++)
        {
            CatSequence[i].Meow();
            yield return new WaitForSeconds(CatSequence[i].shopItem.MeowSound.length+0.1f);
        }
        SetRoundState(roundState = RoundState.roundStarting);
        SetCatsClickable(true);
    }
    public void OnCat_Click(Cat cat)
    {
        if(roundState==RoundState.roundStarting || roundState == RoundState.playing)
            QueueCheck(cat);
        else
            cat.Meow();
    }
    public void CreateSequence(int count)
    {
        CatSequence.Clear();
        for (int i = 0; i <count; i++)
        {
            AddToSequence();
        }
    }
    public void AddNewCat(Cat cat)
    {
        AvailableCats.Add(cat);
    }
    public void AddToSequence()
    {
        int num = UnityEngine.Random.Range(0, AvailableCats.Count);
        Cat cat = AvailableCats[num];
        CatSequence.Add(cat);
        Debug.Log(cat.shopItem.MeowSound.ToSafeString() + " added");
    }
    private void QueueCheck(Cat cat)
    {
        if (CurrentNum== CatSequence.Count) return;
        if(CurrentNum > 0 && roundState != RoundState.playing)
            SetRoundState(RoundState.playing);
        if(cat == CatSequence[CurrentNum])
            OnRightAnswer(cat);
        else
            OnWrongAnswer(cat);
    }
    private void OnRightAnswer(Cat cat)
    {
        cat.Meow();
        if(CurrentNum == CatSequence.Count-1)
        {
           StartCoroutine(OnRihgtAnswer_Coroutine());
        }
        CurrentNum++;
    }
    private IEnumerator OnRihgtAnswer_Coroutine()
    {
        RoundWon.Invoke(CurrentNum);
        audioSource.PlayOneShot(winSound);
        if(PlayFullWinSound)
            yield return new WaitForSeconds(winSound.length+1f);
        else
            yield return new WaitForSeconds(1f);
        audioSource.Stop();
        yield return new WaitForSeconds(0.5f);
        AddToSequence();
        StartRound();
    }
    private void OnWrongAnswer(Cat cat)
    {
        cat.Angry();
        audioSource.PlayOneShot(loseSound);
        SetRoundState(RoundState.lost);
        RoundLost.Invoke(CurrentNum);
    }
    private void SetCatsClickable(bool b)
    {
        foreach (var cat in AvailableCats)
        {
            cat.SetUnclickable(b);
        }
    }
}
