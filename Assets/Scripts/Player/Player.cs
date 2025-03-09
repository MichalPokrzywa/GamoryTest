using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public int maxHP = 100;
    public int currentHP;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Weapon weapon;
    // Start is called before the first frame update
    public void InitPlayer(CharacterGameStats stats)
    {
        maxHP = stats.Hp.GetCalculatedValue();
        currentHP = maxHP;
        playerMovement.walkSpeed = stats.MoveSpeed.GetCalculatedValue();
        playerMovement.runSpeed = playerMovement.walkSpeed * 2;
        WeaponData weaponData = new WeaponData()
        {
            AttackSpeed = stats.AttackSpeed.GetCalculatedValue(),
            CritChance = stats.CritChance.GetCalculatedValue(),
            Damage = stats.Damage.GetCalculatedValue()
        };
        playerMovement.Init();
        weapon.InitWeapon(weaponData);
    }

    public void Damage(int damage)
    {
        currentHP -= damage;
        
        if (currentHP <= 0)
        {
            playerMovement.canMove = false;
            weapon.StopWeapon();
            GameManager.Instance.RestartGame();
        }
        else
        {
            GameManager.Instance.GetGameplayCanvas().UpdateHeath(maxHP, currentHP);
        }
    }
}
