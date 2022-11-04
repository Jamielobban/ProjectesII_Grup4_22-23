using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum WeaponsTypes { SNIPER, GUN, SHOTGUN, UNKNOWN };


public abstract class Weapon /*: MonoBehaviour*/
{    
    protected WeaponsData data;  
    public Weapon(Transform _firePoint, ref SpriteRenderer _sr)
    {        
        data.startReloading = 0f;
        data.timelastPowerupUse = 0;
        data.outOfAmmo = false;
        data.reloading = false;
        data.powerActive = false;
        data.powerupAvailable = false;
        data.firePoint = _firePoint;        
    }
    
    public virtual void Update()
    {        
        CheckShooting();

        InputsUpdate();

        LogicUpdate();

    }

    private void CheckShooting()
    {
        Debug.Log(!data.outOfAmmo && !data.reloading);
        if (!data.outOfAmmo && !data.reloading)
        {
            if (!data.powerActive)
            {                
                if (data.mechanism.Shoot(data.bulletTypePrefab, data.firePoint, data.fireRateinSec))
                {
                    LoadOrReloadWhenNeedIt();                    
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
        if (data.reloading && Time.time - data.startReloading >= data.reloadTimeInSec)
        {
            data.startReloading = 0;
            data.reloading = false;
        }

        if (Time.time - data.timelastPowerupUse >= 20)
        {
            data.powerupAvailable = true;
        }
        else
        {
            data.powerupAvailable = false;
        }
    }

    

    private void InputsUpdate()
    {  

        if (Input.GetButtonDown("UsePowerup"))
        {
            if (data.powerActive || data.powerupAvailable)
            {
               data.powerActive = !data.powerActive;
                Debug.Log("PowerupStateChanged");
                if (!data.powerActive)
                {
                    data.timelastPowerupUse = Time.time;
                }

            }

        }
        else if (Input.GetButtonDown("Reload") && data.currentBulletsInMagazine < data.bulletsPerMagazine)
        {
            data.currentBulletsInMagazine = data.bulletsPerMagazine;
            data.currentMagazines--;
            //AudioManager.Instance.PlaySound(reloadSound, 0.5f);
            data.reloading = true;
            data.startReloading = Time.time;
        }


    }

    private void LoadOrReloadWhenNeedIt()
    {
        data.currentBulletsInMagazine--;

        if(data.currentBulletsInMagazine == 0)
        {
            if(data.currentMagazines == 0)
            {
                data.outOfAmmo = true;
            }
            else
            {
                data.currentBulletsInMagazine = data.bulletsPerMagazine;
                data.currentMagazines--;
                data.reloading = true;
            }
        }
    }

    public bool GetIfOutOffAmmo()
    {
        return data.outOfAmmo;
    }
    
    
}

