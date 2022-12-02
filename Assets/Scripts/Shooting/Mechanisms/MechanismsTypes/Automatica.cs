using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Automatica : Mechanism
{    
    public override bool Shoot(GameObject bulletTypePrefab, Transform firePoint, float fireRateinSec, AudioClip shootSound, float amplitudeGain, float damageMultiplier)
    {
        if (Input.GetButton("Shoot") && Time.time - timeLastShoot >= fireRateinSec)
        {
            GameObject bullet = GameObject.Instantiate(bulletTypePrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().ApplyMultiplierToDamage(damageMultiplier);
            AudioManager.Instance.PlaySound(shootSound, firePoint.position);
            timeLastShoot = Time.time;
            return true;
        }
        return false;
    }
    //public override float GetFireRateMultiplier(float min, float max)
    //{
    //    if (min == 225 && max == 275)
    //    {
    //        return 1.7f;
    //    }
    //    else if (min == 660 && max == 700)
    //    {
    //        return 1.34f;

    //    }

    //    return 0.0f;

    //    //switch (typeWeapon)
    //    //{
    //    //    case WeaponsTypes.GUN:
    //    //        return 1.34f;
    //    //        break;
    //    //    case WeaponsTypes.SHOTGUN:
    //    //        return 1.7f;
    //    //        break;
    //    //    default:
    //    //        break;
    //    //}
    //}
}
