using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class SoundQueueController : MonoBehaviour
{
    public static SoundQueueController instance;
    [SerializeField]
    public int StartedSequenceLength;
    public Cat[] AvailableCats;
    public List<Cat> CatSequence;
    public AudioSource audioSource;
    public AudioClip loseSound;
    public AudioClip winSound;
    public bool PlayFullWinSound;
    private int CurrentNum;
    private void Start()
    {
        audioSource =GetComponent<AudioSource>();
    }
    private void Awake()
    {
        instance=this;    
    }
    public void StartNewGame()
    {
        CreateSequence(StartedSequenceLength);
        StartRound();
    }
    public void StartRound()
    {
        CurrentNum = 0;
        StartCoroutine(PlayStartSoundSequence());
    }
    private IEnumerator PlayStartSoundSequence()
    {
        foreach (Cat cat in AvailableCats)
        {
            cat.gameObject.GetComponent<Clickable>().clickAvailable = false;
        }
        for (int i = 0; i < CatSequence.Count; i++)
        {
            CatSequence[i].Play();
            yield return new WaitForSeconds(CatSequence[i].MeowSound.length);
        }
        Debug.Log("Sounds have played");
        foreach (Cat cat in AvailableCats)
        {
            cat.gameObject.GetComponent<Clickable>().clickAvailable = true;
        }
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
        if(cat == CatSequence[CurrentNum])
        {
            OnRightAnswer();
        }
        else
        {
            OnWrongAnswer();
        }
    }
    private void OnRightAnswer()
    {
        if(CurrentNum == CatSequence.Count-1)
        {
           StartCoroutine(OnRoundWin());
        }
        CurrentNum++;
        //else
        //{
        //    audioSource.PlayOneShot(loseSound);  
        //}
    }
    private IEnumerator OnRoundWin()
    {
        audioSource.PlayOneShot(winSound);
        if(PlayFullWinSound)
            yield return new WaitForSeconds(winSound.length+1f);
        else
            yield return new WaitForSeconds(1f);
        audioSource.Stop();
        AddToSequence();
        StartRound();
    }
    private void OnWrongAnswer()
    {
        CurrentNum = 0;
        audioSource.PlayOneShot(loseSound);
    }
}
