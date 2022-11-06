using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightHand : MonoBehaviour
{
    Weapon weaponInHand, nextWeapon;
    [SerializeField] Transform firePoint;
    [SerializeField]  SpriteRenderer sr;

    //private delegate void OnPowerupDelegate();
    //private OnPowerupDelegate actionOnPowerup;

    
    

    private void Start()
    {
        nextWeapon = WeaponGenerator.Instance.SetMyInitialWeaponAndReturnMyNext(ref weaponInHand, firePoint, ref sr);
        weaponInHand.SetWeaponHand(ref sr);
              

    }

    private void Update()
    {       
        weaponInHand.Update();

        if (weaponInHand.GetIfOutOffAmmo())
        {
            weaponInHand = nextWeapon;
            weaponInHand.SetWeaponHand(ref sr);
            nextWeapon = WeaponGenerator.Instance.ReturnMyNextWeapon(firePoint, ref sr);
        }
    }



}
