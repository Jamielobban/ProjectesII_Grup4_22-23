using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    protected override void CheckPowerUpShooting()
    {
        weaponBulletTypePrefab.GetComponent<Bullet>().powerUpOn = true;
        mechanism.Shoot(weaponBulletTypePrefab, firePoint, weaponShoot, fireRateinSec); //firerate esta en dpm
    }   


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        myType = WeaponsTypes.SNIPER;

        bulletsPerMagazine = 9;
        magazines = 3;
        reloadTimeInSec = 3.8f;

        fireRateinSec = Random.Range(100f, 200f) * this.mechanism.GetFireRateMultiplier(myType);
        

        currentBulletsInMagazine = bulletsPerMagazine;
        currentMagazines = magazines;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    
}
 