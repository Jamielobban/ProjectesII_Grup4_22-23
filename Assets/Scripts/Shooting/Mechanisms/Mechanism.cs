using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mechanism : MonoBehaviour
{
    protected float timeLastShoot;
    

    private void Start()
    {
        timeLastShoot = 0;
    }
    public virtual void Shoot(GameObject bulletTypePrefab ,Transform firePoint, AudioClip shootSound, float fireRateinSec)
    {
        if (Input.GetButtonDown("Shoot") && Time.time - timeLastShoot >= fireRateinSec)
        {
            GameObject bullet = Instantiate(bulletTypePrefab, firePoint.position, firePoint.rotation);
            AudioManager.Instance.PlaySound(shootSound);
        }
          
    }

    public abstract float GetFireRateMultiplier(WeaponsTypes typeWeapon);

}
  