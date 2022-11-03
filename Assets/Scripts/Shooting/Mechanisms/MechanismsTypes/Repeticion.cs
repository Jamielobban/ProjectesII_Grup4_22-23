using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeticion : Mechanism
{
    public override void Shoot(GameObject bulletTypePrefab, Transform firePoint, AudioClip shootSound, float fireRateinSec)
    {
        
    }

    public override float GetFireRateMultiplier(WeaponsTypes typeWeapon)
    {
        switch (typeWeapon)
        {
            case WeaponsTypes.GUN:
                return 0.7f;
                break;
            case WeaponsTypes.SHOTGUN:
                return 0.35f;
                break;
            case WeaponsTypes.SNIPER:
                return 0.5f;
                break;
            default:
                break;
        }
        return 0.0f;
    }
}
