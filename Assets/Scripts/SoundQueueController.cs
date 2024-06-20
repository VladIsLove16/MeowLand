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

public class SoundQueueController : MonoBehaviour
{
    public static SoundQueueController instance;
    [SerializeField]
    public int StartedSequenceLength;
    public Cat[] AvailableCats;
    public List<Cat> CatSequence;
    [HideInInspector]
    private AudioSource audioSource;
    public AudioClip loseSound;
    public AudioClip winSound;
    public bool PlayFullWinSound;
    public bool catsClickable;
    public UnityEvent<RoundState> roundStateChanged;
    private int CurrentNum;
    private RoundState roundState;
    public enum RoundState
    {
        gameStarting,
        playingSounds,
        roundStarting,
        playing,
        lost,
    }
    private void Start()
    {
        audioSource =GetComponent<AudioSource>();
    }
    private void Awake()
    {
        instance=this; 
        
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
                if (hit && catsClickable)
                {
                    Cat cat = hit.collider.GetComponent<Cat>();
                    if (cat != null)
                    {
                        cat.Play();
                        //SoundQueueController.instance.OnCatClick(this);
                        OnCatClick(cat);
                    }

                }
            }
        }

    }
    public void SetUpGame(Cat[] AvailableCats, int StartedSequenceLength)
    {
        this.AvailableCats = AvailableCats;
        this.StartedSequenceLength = StartedSequenceLength;
    }
    public void StartNewGame()
    {
        ScoreManager.instance.ResetPoints();
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
    public void OnCatClick(Cat cat)
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
    public void AddToSequence()
    {
        int num = UnityEngine.Random.Range(0, AvailableCats.Length);
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
           StartCoroutine(OnRoundWin());
        }
        CurrentNum++;
    }
    private IEnumerator OnRoundWin()
    {
        ScoreManager.instance.Set(CatSequence.Count);
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
        CurrentNum = 0;
        audioSource.PlayOneShot(loseSound);
        SetCatsClickable(false);
        SetRoundState(roundState = RoundState.lost);
    }
    private void SetCatsClickable(bool b)
    {
        catsClickable = b;
    }
}
