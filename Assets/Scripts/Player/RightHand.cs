using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    Weapon weaponInHand, nextWeapon;
    [SerializeField] Transform firePoint;
    [SerializeField]
    SpriteRenderer sr;

    private void Start()
    {
        Debug.Log("Start");
        nextWeapon = WeaponGenerator.Instance.SetMyInitialWeaponAndReturnMyNext(ref weaponInHand, firePoint, ref sr);
        
    }

    private void Update()
    {
        Debug.Log("Update");
        weaponInHand.Update();

        if (weaponInHand.GetIfOutOffAmmo())
        {
            weaponInHand = nextWeapon;
            nextWeapon = WeaponGenerator.Instance.ReturnMyNextWeapon();
        }
    }



}
