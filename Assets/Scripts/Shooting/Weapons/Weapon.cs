using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponsTypes { SNIPER, GUN, SHOTGUN, UNKNOWN };

public abstract class Weapon : MonoBehaviour
{

    protected Mechanism mechanism;
    protected WeaponsTypes myType = WeaponsTypes.UNKNOWN;












































    protected int bulletsPerMagazine;
    protected int magazines;
    protected float reloadTimeInSec;
    protected float fireRateinSec;
    protected bool hasBoltSound;
    protected bool powerActive;

    protected int currentBulletsInMagazine;
    protected int currentMagazines;

    private float startReloading;
    private float timeLastShoot;
    private float timeLastPowerShoot;
    private bool reloading;
    private bool outOfAmmo;
    [SerializeField]
    protected Transform firePoint;
    [SerializeField]
    protected GameObject weaponBulletTypePrefab;
    [SerializeField] protected AudioClip weaponShoot, boltSound, reloadSound;
    
    


    //private void Shoot()
    //{
    //    GameObject bullet = Instantiate(weaponBulletTypePrefab, firePoint.position, firePoint.rotation);
    //    weaponBulletTypePrefab.GetComponent<Bullet>().powerUpOn = true;

    //    if (powerActive)
    //        bullet.GetComponent<Bullet>().powerUpOn = true;
    //    else
    //        bullet.GetComponent<Bullet>().powerUpOn = false;

        //    AudioManager.Instance.PlaySound(weaponShoot);        


        //    if (currentBulletsInMagazine != 0)
        //        currentBulletsInMagazine--;

        //    if(currentBulletsInMagazine == 0)
        //    {
        //        if(currentMagazines != 0)
        //        {
        //            currentBulletsInMagazine = bulletsPerMagazine;
        //            currentMagazines--;                
        //            AudioManager.Instance.PlaySound(reloadSound, 0.5f);
        //            reloading = true;
        //            startReloading = Time.time;
        //        }
        //        else
        //        {
        //            outOfAmmo = true;
        //        }
        //    }
        //    else
        //    {
        //        if (hasBoltSound)
        //        {                
        //            AudioManager.Instance.PlaySound(boltSound, 0.5f);
        //        }
        //    }


        //    timeLastShoot = Time.time;

   // }


    protected virtual void Start()
    {
        timeLastShoot = 0;
        timeLastPowerShoot = 0;
        startReloading = 0;
        outOfAmmo = false;
        reloading = false;
        powerActive = false;
    }

    

    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log(powerActive);        
        CheckShooting();

        InputsUpdate();

        LogicUpdate();



    }


    private void CheckShooting()
    {
        if (!outOfAmmo && !reloading)
        {
            if (!powerActive)
            {
                weaponBulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
                mechanism.Shoot(weaponBulletTypePrefab, firePoint, weaponShoot, fireRateinSec);
            }
            else
            {
                CheckPowerUpShooting();
            }            
            
        }
    }

    protected abstract void CheckPowerUpShooting();    

    private void LogicUpdate()
    {
        if (reloading && Time.time - startReloading >= reloadTimeInSec)
        {
            startReloading = 0;
            reloading = false;
        }

        if (Time.time - timeLastPowerShoot >= 20)
        {
            Debug.Log("PowerUpAvailable");
        }
        else
        {
            Debug.Log("PowerUpSleeping");
        }
    }

    

    private void InputsUpdate()
    {  

        if (Input.GetButtonDown("UsePowerup"))
        {
            Debug.Log("PowerupStateChanged");
            powerActive = !powerActive;
        }
        else if (Input.GetButtonDown("Reload") && currentBulletsInMagazine < bulletsPerMagazine)
        {
            currentBulletsInMagazine = bulletsPerMagazine;
            currentMagazines--;
            AudioManager.Instance.PlaySound(reloadSound, 0.5f);
            reloading = true;
            startReloading = Time.time;
        }


    }

    
}

