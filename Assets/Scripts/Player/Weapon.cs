using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponData weaponData;
    private float timeFromLastShot = 99f;
    private bool initWeapon = false;

    public void InitWeapon(WeaponData data)
    {
        weaponData = data;
        initWeapon = true;
    }

    void Update()
    {
        if (initWeapon)
        {
            timeFromLastShot += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    public void StopWeapon()
    {
        initWeapon = false;
    }

    private bool CanShot()
    {
        return timeFromLastShot > (1f / weaponData.AttackSpeed);
    }

    private bool IsACrit()
    {
        return Random.Range(0f, 1f) > weaponData.CritChance;
    }

    private void Shoot()
    {
        if (CanShot())
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo,
                    100.0f))
            {
                IDamagable damagable = hitInfo.transform.GetComponent<IDamagable>();
                if (IsACrit())
                {
                    damagable?.Damage(weaponData.Damage * 2);
                }
                else
                {
                    damagable?.Damage(weaponData.Damage);
                }
            }

            GameManager.Instance.GetGameplayCanvas().ChargeAttack();
            timeFromLastShot = 0;
        }
    }
}

public struct WeaponData
{
    public int Damage;
    public float AttackSpeed;
    public float CritChance;
}