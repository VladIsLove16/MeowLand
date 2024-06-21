using System;
using UnityEngine;

[Serializable]
public class ShopItem : MonoBehaviour
{
    public Guid Id;
    public Money Cost;
    public bool IsUnlocked;
}