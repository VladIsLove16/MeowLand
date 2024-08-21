using TestAppOnWpf.FileSaveSystem;
using YG;
using UnityEngine;
public class YandexGameSaveService : ISaveService
{
    public T LoadData<T>()
    {

        if (typeof(T) == typeof(SaveData))
        {
            return YandexGame.savesData.SaveData;
        }
        else
        {
            Debug.LogAssertion("Cant load not SaveData to YandexGame.SaveData");
            return default(T);
        }
    }

    public void SaveData<T>(T data)
    {
        if (typeof(T) == typeof(SaveData))
        {

        }
        else
        {
            Debug.LogAssertion("Cant save not SaveData to YandexGame.SaveData");
        }
    }
}