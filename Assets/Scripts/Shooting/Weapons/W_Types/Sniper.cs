using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public Sniper()
    {        //bulletType = new SniperBullet();
        bulletsPerMagazine = Random.Range(4,9);
        magazines = Random.Range(2, 4);
        fireRateinSec = Random.Range(100f, 200f) * this.mechanism.GetFireRateMultiplier(100f, 200f); //Aqui esta en dpm
        fireRateinSec /= 60f; //Aqui es dps
        fireRateinSec = 1 / fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        reloadTimeInSec = 3.8f;
        currentBulletsInMagazine = bulletsPerMagazine;
        currentMagazines = magazines;
    }
    protected override void CheckPowerUpShooting()
    {
        bulletTypePrefab.GetComponent<Bullet>().powerUpOn = true;
        mechanism.Shoot(bulletTypePrefab, firePoint, fireRateinSec);
    }

    //protected override float GenerateBaseFireRate()
    //{
    //    throw new System.NotImplementedException();
    //}


    // Start is called before the first frame update
    
    // Update is called once per frame
   

    
}
 