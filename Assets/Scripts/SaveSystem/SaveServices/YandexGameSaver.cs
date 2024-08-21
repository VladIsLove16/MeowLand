using UnityEngine;
using System.Collections;
using YG;
public class YandexGameSaver : MonoBehaviour
{
    private const float AutoSaveInterval = 10f;
    ISaveSystem saveSystem;
    [SerializeField]
    Wallet Wallet;
    [SerializeField]
    ShopDataSaver ShopDataSaver;
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
        saveSystem = new YandexGamesSaveSystem();
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
        var emptyData = new SaveData();
        saveSystem.Save(emptyData);
        Load();
    }

    public void Save()
    {
        //приколы
        SaveData data = new SaveData();

        data.Money = Wallet.Money   ;
        data.ShopData = ShopDataSaver.Get();
        data.HealthData = HealthSystem.GetHealthData();
        

        saveSystem.Save(data);
    }

    private void Load()
    {
        //custom save loader
        SaveData data = saveSystem.Load();

        MoneySaveLoader moneySaveLoader = new MoneySaveLoader();
        moneySaveLoader.SetupData(data.Money, Wallet.Money);
        HealthSystem.Load(data.HealthData);
        ShopDataSaver.Load(data.ShopData);
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

