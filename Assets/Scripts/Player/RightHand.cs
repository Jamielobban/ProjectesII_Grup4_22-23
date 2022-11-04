using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHand : MonoBehaviour
{
    Weapon weaponInHand, nextWeapon;

    private void Start()
    {
        nextWeapon = WeaponGenerator.Instance.SetMyInitialWeaponAndReturnMyNext(ref weaponInHand);
    }

    private void Update()
    {
        if (weaponInHand.outOfAmmo)
        {
            weaponInHand = nextWeapon;
            nextWeapon = WeaponGenerator.Instance.ReturnMyNextWeapon();
        }
    }

}
