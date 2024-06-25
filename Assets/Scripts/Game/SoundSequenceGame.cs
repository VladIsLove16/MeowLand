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
    private int CurrentNum;
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

    public void SetUpGame(List<Cat> AvailableCats, int StartedSequenceLength)
    {
        this.AvailableCats = AvailableCats;
        this.StartedSequenceLength = StartedSequenceLength;
    }
    public void StartNewGame()
    {
        foreach (Cat cat in AvailableCats)
        {
            cat.Clicked.AddListener(() => OnCat_Click(cat));
        }
        CreateSequence(StartedSequenceLength);
        StartRound();
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
            CatSequence[i].Play();
            yield return new WaitForSeconds(CatSequence[i].MeowSound.length);
        }
        SetRoundState(roundState = RoundState.roundStarting);
        SetCatsClickable(true);
    }
    public void OnCat_Click(Cat cat)
    {
        QueueCheck(cat);
    }
    public void CreateSequence(int count)
    {
        CurrentNum = 0;
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
        Debug.Log(cat.MeowSound.ToSafeString() + " added");
    }
    private void QueueCheck(Cat cat)
    {
        if (CurrentNum== CatSequence.Count) return;
        if(CurrentNum > 0 && roundState != RoundState.playing)
            SetRoundState(RoundState.playing);
        if(cat == CatSequence[CurrentNum])
            OnRightAnswer();
        else
            OnWrongAnswer();
    }
    private void OnRightAnswer()
    {
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
    private void OnWrongAnswer()
    {
        RoundLost.Invoke(CurrentNum);
        CurrentNum = 0;
        audioSource.PlayOneShot(loseSound);
        SetCatsClickable(false);
        SetRoundState(RoundState.lost);
    }
    private void SetCatsClickable(bool b)
    {
        foreach (var cat in AvailableCats)
        {
            cat.SetUnclickable(b);
        }
    }
}
