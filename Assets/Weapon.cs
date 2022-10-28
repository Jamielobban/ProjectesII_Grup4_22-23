using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    protected int bulletsPerMagazine;
    protected int magazines;
    protected float reloadTimeInSec;
    protected float fireRateinSec;

    protected int currentBulletsInMagazine;
    protected int currentMagazines;

    private float timeLastShoot;
    private bool reloading;
    private bool outOfAmmo;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject weaponBulletTypePrefab;
    


    private void Shoot()
    {
        GameObject bullet = Instantiate(weaponBulletTypePrefab, firePoint.position, firePoint.rotation);

        if (currentBulletsInMagazine != 0)
            currentBulletsInMagazine--;

        if(currentBulletsInMagazine == 0)
        {
            if(currentMagazines != 0)
            {
                currentBulletsInMagazine = bulletsPerMagazine;
                currentMagazines--;
            }
            else
            {
                outOfAmmo = true;
            }
        }

        timeLastShoot = Time.time;
    }


    protected virtual void Start()
    {
        timeLastShoot = 0;
        outOfAmmo = false;
    }

    

    // Update is called once per frame
    protected virtual void Update()
    {        

        if (Input.GetButtonDown("Fire1") && Time.time - timeLastShoot >= fireRateinSec && !outOfAmmo)
        {
            Debug.Log("in");
            Shoot();
        }
    }
}

//public class PlayerShooting : MonoBehaviour
//{



//    public float bulletForce = 20f;


//    // Update is called once per frame
//    void Update()
//    {
//        
//    }

//    void Shoot()
//    {
//       
//    }
//}