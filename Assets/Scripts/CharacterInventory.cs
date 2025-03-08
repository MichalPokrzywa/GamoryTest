using System.Collections;
using System.Collections.Generic;
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
        stats.HP = tempHP;
        stats.Damage = tempDamage;
        stats.AttackSpeed = tempAttackSpeed;
        stats.CritChance = tempCritChance;
        stats.MoveSpeed = tempMoveSpeed;
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
