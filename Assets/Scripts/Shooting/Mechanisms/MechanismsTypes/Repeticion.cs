using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeticion : Mechanism
{
    public override bool Shoot(GameObject bulletTypePrefab, Transform firePoint, float fireRateinSec)
    {
        if (Input.GetButtonDown("Shoot") && Time.time - timeLastShoot >= fireRateinSec)
        {
            Instantiate(bulletTypePrefab, firePoint.position, firePoint.rotation);
            AudioManager.Instance.PlaySound(shootSound);
            AudioManager.Instance.PlaySound(boltSound, 0.5f);
            timeLastShoot = Time.time;
            return true;
        }
        return false;
    }    

    public override float GetFireRateMultiplier(float min, float max)
    {
        if(min == 100 && max == 200)
        {
            return 0.5f;
        }
        else if(min == 225 && max == 275)
        {
            return 0.35f;
        }
        else if (min == 660 && max == 700)
        {
            return 0.7f;
        }

        return 0.0f;

        //switch (typeWeapon)
        //{
        //    case WeaponsTypes.GUN:
        //        return 0.7f;
        //        break;
        //    case WeaponsTypes.SHOTGUN:
        //        return 0.35f;
        //        break;
        //    case WeaponsTypes.SNIPER:
        //        return 0.5f;
        //        break;
        //    default:
        //        break;
        //}

    }
}
