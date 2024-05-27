using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MySceneManager : MonoBehaviour
{
    public void LoadSceneMode()
    {
        SceneManager.LoadScene("Game");
    }
   
}
