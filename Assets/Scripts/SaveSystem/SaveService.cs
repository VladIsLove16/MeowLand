using UnityEngine;
using System.Collections;
using YG;
using System.Collections.Generic;

namespace CapybaraAdventure.Save
{
    public class SaveService : MonoBehaviour
    {
        private const float AutoSaveInterval = 10f;

        [SerializeField] private Wallet Wallet;
        [SerializeField] private ShopDataSaver ShopDataSaver;

        private YandexGamesSaveSystem _saveSystem;

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
            _saveSystem = new YandexGamesSaveSystem();

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
            Money emptyData = new ();
            ShopData emptyData2 = new ();
            HealthData data = new ();
            SaveData saveData = new SaveData()
            {
                Money  = emptyData,
                ShopData = emptyData2,
                HealthData = data,
            };
            _saveSystem.Save(saveData);
            Load();
        }

        public void Save()
        {
            Money Money = Wallet.Money;
            ShopData ShopData = ShopDataSaver.Get();
            HealthData healthData = HealthSystem.GetHealthData();
            SaveData saveData = new SaveData()
            {
                Money = Money,
                ShopData = ShopData,
                HealthData = healthData,
            };

            _saveSystem.Save(saveData);
        }

        private void Load()
        {
            SaveData data = _saveSystem.Load();

            Wallet.Load(data.Money);
            ShopDataSaver.Load(data.ShopData);
            HealthSystem.Load(data.HealthData);
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
}