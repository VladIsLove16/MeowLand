using YG;
public class YandexGamesSaveSystem : ISaveSystem
{
    public void Save(SaveData data)
    {
        YandexGame.savesData.SaveData = data;
        YandexGame.SaveProgress();
    }

    public SaveData Load()
    {
        return YandexGame.savesData.SaveData;
    }
}