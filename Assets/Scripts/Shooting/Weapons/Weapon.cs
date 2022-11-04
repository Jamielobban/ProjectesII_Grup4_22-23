using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum WeaponsTypes { SNIPER, GUN, SHOTGUN, UNKNOWN };

public abstract class Weapon : MonoBehaviour
{

    protected Mechanism mechanism;
    [SerializeField]
    protected Transform firePoint;
    [SerializeField]protected GameObject bulletTypePrefab; 
    //[SerializeField] protected AudioClip weaponShoot, boltSound, reloadSound;



    //[SerializeField]
    //protected GameObject weaponBulletTypePrefab;

    //protected WeaponsTypes myType = WeaponsTypes.UNKNOWN;

    protected int bulletsPerMagazine;
    protected int magazines;
    protected float reloadTimeInSec;
    protected float fireRateinSec;
    protected bool hasBoltSound;
    protected bool powerActive;

    protected int currentBulletsInMagazine;
    protected int currentMagazines;

    private float startReloading;    
    private bool reloading;
    public bool outOfAmmo;
    private float timelastPowerupUse;
    private bool powerupAvailable;


    public Weapon()
    {        
        startReloading = 0;
        timelastPowerupUse = 0;
        outOfAmmo = false;
        reloading = false;
        powerActive = false;
        powerupAvailable = false;
    }
    
    


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
     

    

    // Update is called once per frame
    void Update()
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
                if (mechanism.Shoot(bulletTypePrefab, firePoint, fireRateinSec))
                {
                    currentBulletsInMagazine--;
                    
                }
                
            }
            else
            {               
                
                CheckPowerUpShooting();
               
            }            
            
        }
    }

    protected abstract void CheckPowerUpShooting();    
    //protected abstract float GenerateBaseFireRate();    

    private void LogicUpdate()
    {
        if (reloading && Time.time - startReloading >= reloadTimeInSec)
        {
            startReloading = 0;
            reloading = false;
        }

        if (Time.time - timelastPowerupUse >= 20)
        {
            powerupAvailable = true;
        }
        else
        {
            powerupAvailable = false;
        }
    }

    

    private void InputsUpdate()
    {  

        if (Input.GetButtonDown("UsePowerup"))
        {
            if (powerActive || powerupAvailable)
            {
               powerActive = !powerActive;
                Debug.Log("PowerupStateChanged");
                if (!powerActive)
                {
                    timelastPowerupUse = Time.time;
                }

            }

        }
        else if (Input.GetButtonDown("Reload") && currentBulletsInMagazine < bulletsPerMagazine)
        {
            currentBulletsInMagazine = bulletsPerMagazine;
            currentMagazines--;
            //AudioManager.Instance.PlaySound(reloadSound, 0.5f);
            reloading = true;
            startReloading = Time.time;
        }


    }

    private void LoadOrReloadWhenNeedIt()
    {
        currentBulletsInMagazine--;

        if(currentBulletsInMagazine == 0)
        {
            if(currentMagazines == 0)
            {
                outOfAmmo = true;
            }
            else
            {
                currentBulletsInMagazine = bulletsPerMagazine;
                currentMagazines--;
                reloading = true;
            }
        }
    }

    
}

