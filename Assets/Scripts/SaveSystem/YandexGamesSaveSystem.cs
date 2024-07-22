using System.Collections.Generic;
using YG;

namespace CapybaraAdventure.Save
{
    public class YandexGamesSaveSystem : ISaveSystem
    {
        public void Save(SaveData data)
        {
            YandexGame.savesData.Money = data.Money;
            YandexGame.savesData.ShopData = data.ShopData;
            YandexGame.savesData.HealthData = data.HealthData;
            YandexGame.SaveProgress();
        }

        public SaveData Load()
        {
            return new SaveData()
            {
                Money = YandexGame.savesData.Money,
                ShopData = YandexGame.savesData.ShopData,
                HealthData = YandexGame.savesData.HealthData
            };
        }
    }
}