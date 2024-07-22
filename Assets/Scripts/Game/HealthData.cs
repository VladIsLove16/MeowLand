
using System;

public class HealthData
{
    public HealthData(DateTime date,int health)
    {
        LastTimeRestored = date;
        Health  = health;
    }
    public HealthData()
    {
        LastTimeRestored = DateTime.Now;
        Health = HealthSystem.MaxHealth;
    }
    public System.DateTime LastTimeRestored ;
    public int Health;
}