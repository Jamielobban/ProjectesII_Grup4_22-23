using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Seamiautomatica : Mechanism
{
    

    public override bool Shoot(GameObject bulletTypePrefab, Transform firePoint, float fireRateinSec, AudioClip shootSound, float amplitudeGain, float damageMultiplier)
    {
        if (Input.GetButtonDown("Shoot") && Time.time - timeLastShoot >= fireRateinSec)
        {
            GameObject bullet = GameObject.Instantiate(bulletTypePrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().ApplyMultiplierToDamage(damageMultiplier);
            AudioManager.Instance.PlaySound(shootSound, firePoint.position);
            //CinemachineShake.Instance.ShakeCamera(5f, .1f);
            timeLastShoot = Time.time;
            CinemachineShake.Instance.ShakeCamera(5f*amplitudeGain, .1f);
            return true;
        }
        return false;
    }
    


    //public override float GetFireRateMultiplier(float min, float max)
    //{
    //    if (min == 100 && max == 200)
    //    {
    //        return 1.5f;
    //    }
    //    else if (min == 225 && max == 275)
    //    {
    //        return 1.01f;
    //    }
    //    else if (min == 660 && max == 700)
    //    {
    //        return 1.02f;
    //    }

    //    return 0.0f;

    //    //switch (typeWeapon)
    //    //{
    //    //    case WeaponsTypes.GUN:
    //    //        return 1.02f;
    //    //        break;
    //    //    case WeaponsTypes.SHOTGUN:
    //    //        return 1.01f;
    //    //        break;
    //    //    case WeaponsTypes.SNIPER:
    //    //        break;
    //    //        return 2.0f;
    //    //    default:
    //    //        break;
    //    //}
        
    //}
   
}
