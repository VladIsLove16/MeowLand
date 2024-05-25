using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [HideInInspector]
    public bool clickAvailable;
    private Cat cat;
    private void Awake()
    {
        cat= GetComponent<Cat>();
        clickAvailable = false;
    }
    private void OnMouseDown()
    {
        if(clickAvailable) {
            cat.Play();
            SoundQueueController.instance.OnCatClick(cat);
        }
    }
}
