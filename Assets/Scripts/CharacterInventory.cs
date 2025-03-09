using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class CharacterInventory : MonoBehaviour
{
    [SerializeField] private CharacterStatsUI characterStatsUI;
    public List<CharacterSlot> itemSlots = new List<CharacterSlot>();
    private CharacterGameStats stats = new CharacterGameStats();

    void Start()
    {
        foreach (CharacterSlot slot in itemSlots)
        {
            slot.OnItemSlotChange += UpdateStats;
        }

        stats.BaseDamage = Random.Range(5, 10);
        stats.BaseHP = Random.Range(10, 25);
        stats.BaseCritChance = Random.Range(0f, 10f);
        stats.BaseAttackSpeed = Random.Range(0.75f, 1f);
        stats.BaseMoveSpeed = Random.Range(4f, 6f);
        characterStatsUI.InitStats(stats);
    }

    private void UpdateStats()
    {
        int tempHP = 0;
        int tempDamage = 0;
        float tempAttackSpeed = 0;
        float tempCritChance = 0;
        float tempMoveSpeed = 0;
        foreach (CharacterSlot slot in itemSlots)
        {
            if (slot.slottedItem != null)
            {
                tempHP += slot.slottedItem.HealthPoints;
                tempDamage += slot.slottedItem.Damage;
                tempAttackSpeed += slot.slottedItem.AttackSpeed;
                tempCritChance += slot.slottedItem.CriticalStrikeChance;
                tempMoveSpeed += slot.slottedItem.MovementSpeed;
            }
        }
        stats.BonusHP = tempHP;
        stats.BonusDamage = tempDamage;
        stats.BonusAttackSpeed = tempAttackSpeed;
        stats.BonusCritChance = tempCritChance;
        stats.BonusMoveSpeed = tempMoveSpeed;
        characterStatsUI.UpdateStatsOnUI(stats);
    }

    public CharacterGameStats GetStats()
    {
        return stats;
    }
    public void LightUpCategory(string Catergory)
    {
        foreach (CharacterSlot slot in itemSlots)
        {
            if (slot.slotType.Equals(Catergory))
            {
                slot.ShowSlot();
            }
        }
    }

    public void LightDownCategories()
    {
        foreach (CharacterSlot slot in itemSlots)
        {
            slot.HideSlot();
        }
    }
}
