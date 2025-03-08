using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class CharacterStatsUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text itemHp;
    [SerializeField] private TMP_Text itemDamage;
    [SerializeField] private TMP_Text itemAttackSpeed;
    [SerializeField] private TMP_Text itemCritChance;
    [SerializeField] private TMP_Text itemMoveSpeed;

    void Start()
    {
        itemHp.text = "0";
        itemDamage.text = "0";
        itemAttackSpeed.text = "0.00%";
        itemCritChance.text = "0.00%";
        itemMoveSpeed.text = "0.00%";
    }


    public void UpdateStatsOnUI(CharacterGameStats stats)
    {
        itemHp.text = stats.HP.ToString();
        itemDamage.text = stats.Damage.ToString();
        itemAttackSpeed.text = stats.AttackSpeed.ToString("0.00") + " %";
        itemCritChance.text = stats.CritChance.ToString("0.00") + " %";
        itemMoveSpeed.text = stats.MoveSpeed.ToString("0.00") + " %";
    }
}
