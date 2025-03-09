using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterStatsUI : MonoBehaviour
{
    [Header("UI Elements")]
    [Header("Base stats")]
    [SerializeField] private TMP_Text baseHp;
    [SerializeField] private TMP_Text baseDamage;
    [SerializeField] private TMP_Text baseAttackSpeed;
    [SerializeField] private TMP_Text baseCritChance;
    [SerializeField] private TMP_Text baseMoveSpeed;

    [Header("Bonus stats form Items")]
    [SerializeField] private TMP_Text itemHp;
    [SerializeField] private TMP_Text itemDamage;
    [SerializeField] private TMP_Text itemAttackSpeed;
    [SerializeField] private TMP_Text itemCritChance;
    [SerializeField] private TMP_Text itemMoveSpeed;

    [Header("Calculated stats")]
    [SerializeField] private TMP_Text calcHp;
    [SerializeField] private TMP_Text calcDamage;
    [SerializeField] private TMP_Text calcAttackSpeed;
    [SerializeField] private TMP_Text calcCritChance;
    [SerializeField] private TMP_Text calcMoveSpeed;

    public void InitStats(CharacterGameStats stats)
    {
        // Base stats
        baseHp.text = stats.Hp.baseValue.ToString();
        baseDamage.text = stats.Damage.baseValue.ToString();
        baseAttackSpeed.text = stats.AttackSpeed.baseValue.ToString("0.00");
        baseCritChance.text = stats.CritChance.baseValue.ToString("0.00") + " %";
        baseMoveSpeed.text = stats.MoveSpeed.baseValue.ToString("0.00");

        // Item stats (all start at 0)
        itemHp.text = "0";
        itemDamage.text = "0";
        itemAttackSpeed.text = "0.00 %";
        itemCritChance.text = "0.00 %";
        itemMoveSpeed.text = "0.00 %";

        // Calculated stats (base + bonus)
        calcHp.text = stats.Hp.GetCalculatedValue().ToString();
        calcDamage.text = stats.Damage.GetCalculatedValue().ToString();
        calcAttackSpeed.text = stats.AttackSpeed.GetCalculatedValue().ToString("0.00");
        calcCritChance.text = stats.CritChance.GetCalculatedValue().ToString("0.00") + " %";
        calcMoveSpeed.text = stats.MoveSpeed.GetCalculatedValue().ToString("0.00");
    }

    public void UpdateStatsOnUI(CharacterGameStats stats)
    {
        // Item stats (bonus values)
        itemHp.text = stats.Hp.bonusValue.ToString();
        itemDamage.text = stats.Damage.bonusValue.ToString();
        itemAttackSpeed.text = stats.AttackSpeed.bonusValue.ToString("0.00") + " %";
        itemCritChance.text = stats.CritChance.bonusValue.ToString("0.00") + " %";
        itemMoveSpeed.text = stats.MoveSpeed.bonusValue.ToString("0.00") + " %";

        // Recalculate and display the total stats (base + bonus)
        calcHp.text = stats.Hp.GetCalculatedValue().ToString();
        calcDamage.text = stats.Damage.GetCalculatedValue().ToString();
        calcAttackSpeed.text = stats.AttackSpeed.GetCalculatedValue().ToString("0.00");
        calcCritChance.text = stats.CritChance.GetCalculatedValue().ToString("0.00") + " %";
        calcMoveSpeed.text = stats.MoveSpeed.GetCalculatedValue().ToString("0.00");
    }
}
