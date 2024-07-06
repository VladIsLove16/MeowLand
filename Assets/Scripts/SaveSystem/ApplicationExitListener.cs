using UnityEngine;

public class ApplicationExitListener : MonoBehaviour
{
    public static ApplicationExitListener instance;
    private void Awake()
    {
        instance=this;
    }
    // Этот метод вызывается, когда приложение или игра закрывается
    void OnApplicationQuit()
    {
        // Выполняем нужные действия перед выходом из приложения
        Debug.Log("Application is quitting...");
        HealthSystem.SaveData();
        // Ваш код здесь
        PerformCleanup();
    }

    // Пример метода для выполнения необходимых действий
    private void PerformCleanup()
    {
        // Здесь вы можете выполнять необходимые действия, такие как сохранение данных или освобождение ресурсов
        Debug.Log("Performing cleanup tasks...");
        // Например, сохранить данные:
        SaveGameData();
    }

    // Пример метода для сохранения данных
    private void SaveGameData()
    {
        // Ваш код для сохранения данных здесь
        Debug.Log("Saving game data...");
    }
}
