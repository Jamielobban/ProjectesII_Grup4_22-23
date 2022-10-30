using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

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
    private Transform firePoint;
    [SerializeField]
    private GameObject weaponBulletTypePrefab;
    [SerializeField]
    private AudioSource weaponShoot;
    [SerializeField]
    private AudioSource boltSound;
    [SerializeField]
    private AudioSource reloadSound;
    


    private void Shoot()
    {
        GameObject bullet = Instantiate(weaponBulletTypePrefab, firePoint.position, firePoint.rotation);


        if (powerActive)
            bullet.GetComponent<Bullet>().powerUpOn = true;
        else
            bullet.GetComponent<Bullet>().powerUpOn = false;


        weaponShoot.Play();


        if (currentBulletsInMagazine != 0)
            currentBulletsInMagazine--;

        if(currentBulletsInMagazine == 0)
        {
            if(currentMagazines != 0)
            {
                currentBulletsInMagazine = bulletsPerMagazine;
                currentMagazines--;
                reloadSound.PlayDelayed(0.5f);
                reloading = true;
                startReloading = Time.time;
            }
            else
            {
                outOfAmmo = true;
            }
        }
        else
        {
            if (hasBoltSound)
            {
                boltSound.PlayDelayed(0.5f);
            }
        }


        timeLastShoot = Time.time;
        
    }


    protected virtual void Start()
    {
        timeLastShoot = 0;
        timeLastPowerShoot = 0;
        startReloading = 0;
        outOfAmmo = false;
        reloading = false;
        powerActive = true;
    }

    

    // Update is called once per frame
    protected virtual void Update()
    {
        //Debug.Log(powerActive);

        if(reloading && Time.time - startReloading >= reloadTimeInSec)
        {
            startReloading = 0;
            reloading = false;
        }


        if (Input.GetButtonDown("Fire1") && Time.time - timeLastShoot >= fireRateinSec && !outOfAmmo && !reloading)
        {            
            Shoot();
        }
        else if(Input.GetButtonDown("Fire2") && Time.time - timeLastPowerShoot >= 20 && !powerActive)
        {
            powerActive = true;
        }
    }
}

