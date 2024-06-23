using System;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngineInternal;
public static class HealthSystem
{
    public static int Health { get; private set; } = 8;
    public static UnityEvent<int> healthChanged=new();
    public static int MaxHealth { get; private set; } = 9;
    public static TimeSpan TimeToHeal = TimeSpan.FromMinutes(15); 
    public static DateTime LastTimeRestored = DateTime.Now;
    public static TimeSpan TimePassed = TimeSpan.Zero;
    public static string TimeLeftString
    {
        get
        {
            TimeSpan a = GetTimeToHealLeft();
            return (int)a.TotalMinutes+":"+(int)a.TotalSeconds%60;
        }
    }
    public static TimeSpan GetTimeToHealLeft()
    {
        UpdateTimePassed();
        return TimeToHeal - TimePassed;
    }
    public static void LoseHealth(int amount = 1)
    {
        if(amount>0 && Health==MaxHealth)
        {
            LastTimeRestored = DateTime.Now;
        }
        Health-=amount;
        healthChanged.Invoke(Health);
    }
    public static void Heal(int amount = 1)
    {
        Health+= amount;
        Health = Math.Max(MaxHealth, Health);
        healthChanged.Invoke(Health);
    }
    private static void HealByTime()
    {
        int roundedHealth =(int)(TimePassed / TimeToHeal);
        int mod = (int)(TimePassed.TotalSeconds % TimeToHeal.TotalSeconds);
        if (roundedHealth > 0)
        {
            Heal(roundedHealth);
            LastTimeRestored = DateTime.Now.AddSeconds(-mod);
        }
    }
    private static void UpdateTimePassed()
    {
        if (!loaded) { loaded = true;try {
                HealthData a = SaveSystem.Load<HealthData>();
                LastTimeRestored = a.LastTimeRestored;
                Health = a.Health;

            } catch { } }
        TimePassed = DateTime.Now - LastTimeRestored;
        if (TimePassed > TimeToHeal)
            HealByTime();
    }
    private static bool loaded = false;
    private static void OnApplicationQuit()
    {
        SaveSystem.Save<HealthData>(new HealthData(LastTimeRestored,Health)); ;
    }
}
