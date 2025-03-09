using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        CreateCharacterStats();
        characterStatsUI.InitStats(stats);
    }

    public CharacterSlot GetCharacterSlotByCategory(string type)
    {
        return itemSlots.First(slot => slot.slotType == type);
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

    private void UpdateStats()
    {
        int tempHP = 0;
        int tempDamage = 0;
        float tempAttackSpeed = 0;
        float tempCritChance = 0;
        float tempMoveSpeed = 0;
        foreach (CharacterSlot slot in itemSlots.Where(slot => slot.slottedItem != null))
        {
            tempHP += slot.slottedItem.HealthPoints;
            tempDamage += slot.slottedItem.Damage;
            tempAttackSpeed += slot.slottedItem.AttackSpeed;
            tempCritChance += slot.slottedItem.CriticalStrikeChance;
            tempMoveSpeed += slot.slottedItem.MovementSpeed;
        }

        stats.Hp.bonusValue = tempHP;
        stats.Damage.bonusValue = tempDamage;
        stats.AttackSpeed.bonusValue = tempAttackSpeed;
        stats.CritChance.bonusValue = tempCritChance;
        stats.MoveSpeed.bonusValue = tempMoveSpeed;
        characterStatsUI.UpdateStatsOnUI(stats);
    }

    private void CreateCharacterStats()
    {
        stats.Damage.baseValue = Random.Range(5, 10);
        stats.Hp.baseValue = Random.Range(10, 25);
        stats.CritChance.baseValue = Random.Range(0f, 10f);
        stats.AttackSpeed.baseValue = Random.Range(0.75f, 1f);
        stats.MoveSpeed.baseValue = Random.Range(4f, 6f);
    }
}