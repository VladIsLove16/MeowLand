using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Cat : MonoBehaviour
{
    public int SceneID;
    public string Name;
    [SerializeField]
    public AudioClip MeowSound;
    private AudioSource AudioSource;
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();   
    }
    public void Play()
    {
        AudioSource.PlayOneShot(MeowSound);
    }
}
