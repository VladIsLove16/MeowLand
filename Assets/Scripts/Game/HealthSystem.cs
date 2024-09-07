using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngineInternal;
public static class HealthSystem
{
    public static int Health { get; private set; } = 9;
    public static UnityEvent<int> healthLost=new();
    public static UnityEvent<int> healthHealed=new();
    public static readonly int MaxHealth = 9;
    public static TimeSpan TimeToHeal = TimeSpan.FromMinutes(15); 
    public static DateTime LastTimeRestored = DateTime.Now;
    public static TimeSpan TimePassed = TimeSpan.Zero;
    private static bool Loaded =false;
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
        Health-=amount;
        Health = Math.Clamp(Health,0, MaxHealth); 
        if (amount > 0 && Health == MaxHealth)
        {
            LastTimeRestored = DateTime.Now;
        }
        healthLost.Invoke(Health);
    }
    public static void Heal(int amount = 1)
    {
        Health+= amount;
        Health = Math.Clamp(Health, 0, MaxHealth);
        healthHealed.Invoke(Health);
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
        TimePassed = DateTime.Now - LastTimeRestored;
        if (TimePassed > TimeToHeal)
            HealByTime();
    }

    public static void Load(HealthData data)
    {
        DateTime last = data.LastTimeRestored;
        LastTimeRestored = last;
        Health = data.Health;
    }

    internal static HealthData GetHealthData()
    {
        return new HealthData(LastTimeRestored, Health);
    }

    private static bool loaded = false;
}
