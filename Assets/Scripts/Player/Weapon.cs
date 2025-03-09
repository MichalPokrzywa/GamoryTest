using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private WeaponData weaponData;
    private float timeFromLastShot = 99f;
    private bool initWeapon = true;
    void Start()
    {
        weaponData.AttackSpeed = 1f;
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

    private bool CanShot()
    {
        return timeFromLastShot > (1f / weaponData.AttackSpeed);
    }

    private void Shoot()
    {
        if (CanShot())
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo,
                    100.0f))
            {
                Debug.Log(hitInfo.transform.name);
            }

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