using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterGameStats
{
    public int BaseHP;
    public int BonusHP = 0;
    public int BaseDamage;
    public int BonusDamage = 0;
    public float BaseAttackSpeed;
    public float BonusAttackSpeed = 0f;
    public float BaseCritChance;
    public float BonusCritChance = 0f;
    public float BaseMoveSpeed;
    public float BonusMoveSpeed = 0f;

    public int GetCalculatedHP()
    {
        return BaseHP + BonusHP;
    }

    public int GetCalculatedDamage()
    {
        return BaseDamage + BonusDamage;
    }
    public float GetCalculatedAttackSpeed()
    {
        return BaseAttackSpeed + BaseAttackSpeed * (BonusAttackSpeed/100) ;
    }
    public float GetCalculatedCritChance()
    {
        return BaseCritChance + BonusCritChance;
    }
    public float GetCalculatedMoveSpeed()
    {
        return BaseMoveSpeed + BaseMoveSpeed * (BonusMoveSpeed / 100);
    }
}
