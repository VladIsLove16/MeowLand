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
    public static YandexGameSaver Instance;
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
        if ((Instance==null))
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        saveSystem = new YandexGamesSaveSystem();
        if (YandexGame.SDKEnabled == true)
        {
            Load();
            StartCoroutine(AutoSave());
        }
        Wallet.moneyChanged.AddListener(Save);
        DontDestroyOnLoad(gameObject);
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
        Debug.Log("saveProcess");
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

