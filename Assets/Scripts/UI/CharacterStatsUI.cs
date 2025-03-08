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
        baseHp.text = stats.BaseHP.ToString();
        baseDamage.text = stats.BaseDamage.ToString();
        baseAttackSpeed.text = stats.BaseAttackSpeed.ToString("0.00");
        baseCritChance.text = stats.BaseCritChance.ToString("0.00") + " %";
        baseMoveSpeed.text = stats.BaseMoveSpeed.ToString("0.00");
        itemHp.text = "0";
        itemDamage.text = "0";
        itemAttackSpeed.text = "0.00 %";
        itemCritChance.text = "0.00 %";
        itemMoveSpeed.text = "0.00 %";
        calcHp.text = stats.GetCalculatedHP().ToString();
        calcDamage.text = stats.GetCalculatedDamage().ToString();
        calcAttackSpeed.text = stats.GetCalculatedAttackSpeed().ToString("0.00");
        calcCritChance.text = stats.GetCalculatedCritChance().ToString("0.00") + " %";
        calcMoveSpeed.text = stats.GetCalculatedMoveSpeed().ToString("0.00");
    }

    public void UpdateStatsOnUI(CharacterGameStats stats)
    {
        itemHp.text = stats.BonusHP.ToString();
        itemDamage.text = stats.BonusDamage.ToString();
        itemAttackSpeed.text = stats.BonusAttackSpeed.ToString("0.00") + " %";
        itemCritChance.text = stats.BonusCritChance.ToString("0.00") + " %";
        itemMoveSpeed.text = stats.BonusMoveSpeed.ToString("0.00") + " %";
        calcHp.text = stats.GetCalculatedHP().ToString();
        calcDamage.text = stats.GetCalculatedDamage().ToString();
        calcAttackSpeed.text = stats.GetCalculatedAttackSpeed().ToString("0.00");
        calcCritChance.text = stats.GetCalculatedCritChance().ToString("0.00") + " %";
        calcMoveSpeed.text = stats.GetCalculatedMoveSpeed().ToString("0.00");
    }
}
