using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automatica : Mechanism
{    
    public override void Shoot(GameObject bulletTypePrefab, Transform firePoint, AudioClip shootSound, float fireRateinSec)
    {
        
    }

    public override float GetFireRateMultiplier(WeaponsTypes typeWeapon)
    {
        switch (typeWeapon)
        {
            case WeaponsTypes.GUN:
                return 1.34f;
                break;
            case WeaponsTypes.SHOTGUN:
                return 1.7f;
                break;            
            default:
                break;
        }
        return 0.0f;
    }
}
