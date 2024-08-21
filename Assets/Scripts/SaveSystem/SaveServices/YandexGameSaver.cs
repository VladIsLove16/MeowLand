using UnityEngine;
using System.Collections;
using YG;
using TestAppOnWpf.FileSaveSystem;
using System.Collections.Generic;
using TestAppOnWpf.SaveLoaderSystem;
using System;
public class YandexGameSaver : MonoBehaviour
{
    private const float AutoSaveInterval = 10f;
    #region MonoBehaviour

    private void OnEnable()
    {
        YandexGame.GetDataEvent += Load;
    }
    private void OnDisable()
    {
        YandexGame.GetDataEvent -= Load;
    }
    private void Awake()
    {
        if (YandexGame.SDKEnabled == true)
        {
            Load();
            StartCoroutine(AutoSave());
        }
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    #endregion

    public void ResetProcess()
    {
        SaveData saveData = new();
        Load();
    }

    public void Save()
    {
    }

    private void Load()
    {
        YandexGame.savesData.SaveData = new();
    }
        
    private IEnumerator AutoSave()
    {
        while (true)
        {
            Save();
            yield return new WaitForSeconds(AutoSaveInterval);
        }
    }
}

