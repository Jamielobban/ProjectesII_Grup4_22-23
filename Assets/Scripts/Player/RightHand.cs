using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    Weapon weaponInHand, nextWeapon;
    [SerializeField] Transform firePoint;

    private void Start()
    {
        Debug.Log("Start");
        nextWeapon = WeaponGenerator.Instance.SetMyInitialWeaponAndReturnMyNext(ref weaponInHand, firePoint);
        
    }

    private void Update()
    {
        Debug.Log("Update");
        weaponInHand.Update();

        if (weaponInHand.outOfAmmo)
        {
            weaponInHand = nextWeapon;
            nextWeapon = WeaponGenerator.Instance.ReturnMyNextWeapon();
        }
    }



}
