using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name;
    public string Category;
    public int Rarity;
    public int Damage;
    public int HealthPoints;
    public int Defense;
    public float LifeSteal;
    public float CriticalStrikeChance;
    public float AttackSpeed;
    public float MovementSpeed;
    public float Luck;
    public Sprite Icon;
}
[System.Serializable]
public class ItemData
{
    public List<Item> Items;
}