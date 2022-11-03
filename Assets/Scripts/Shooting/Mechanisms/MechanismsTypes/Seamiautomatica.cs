using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seamiautomatica : Mechanism
{
    

    public override void Shoot(GameObject bulletTypePrefab, Transform firePoint, AudioClip shootSound, float fireRateinSec)
    {
        

        
    }


    public override float GetFireRateMultiplier(WeaponsTypes typeWeapon)
    {
        switch (typeWeapon)
        {
            case WeaponsTypes.GUN:
                return 1.02f;
                break;
            case WeaponsTypes.SHOTGUN:
                return 1.01f;
                break;
            case WeaponsTypes.SNIPER:
                break;
                return 2.0f;
            default:
                break;
        }
        return 0.0f;
    }
}
