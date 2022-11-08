using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum WeaponsTypes { SNIPER, GUN, SHOTGUN, UNKNOWN };


public abstract class Weapon /*: MonoBehaviour*/
{    
    protected WeaponsData data;    

    public Weapon(Transform _firePoint)
    {        
        data.startReloading = 0f;
        data.timelastPowerupExit = 0.0f;
        data.timelastPowerupEnter = 0;
        data.outOfAmmo = false;
        data.reloading = false;
        data.powerActive = false;
        data.powerupAvailable = false;
        data.firePoint = _firePoint;
    }
    
    public virtual void Update()
    {
        if (data.powerupAvailable)
        {
            //Debug.Log("available");
        }    
             

        CheckShooting();

        InputsUpdate();

        LogicUpdate();

    }

    private void CheckShooting()
    {
        
        if (!data.outOfAmmo && !data.reloading)
        {
            if (!data.powerActive)
            {                
                if (data.mechanism.Shoot(data.bulletTypePrefab, data.firePoint, data.fireRateinSec, data.shootSound))
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
    public void SetWeaponHand(ref SpriteRenderer _sr)
    {
        _sr.sprite = data.weaponSprite;
        _sr.color = data.weaponColor;
    }
    //protected abstract float GenerateBaseFireRate();    

    private void LogicUpdate()
    {
        if (data.reloading && Time.time - data.startReloading >= data.reloadTimeInSec+0.5f)
        {           
            data.reloading = false;
        }

        if (Time.time - data.timelastPowerupExit >= 5 && !data.powerupAvailable)
        {
            data.powerupAvailable = true;            
        }       
    }

    

    private void InputsUpdate()
    {  

        if (Input.GetButtonDown("UsePowerup"))
        {
            if (!data.powerActive && data.powerupAvailable)
            {
                data.powerActive = true;
                ActionOnEnterPowerup();
                data.timelastPowerupEnter = Time.time;               
            }

        }
        else if (Input.GetButtonDown("Reload") && data.currentBulletsInMagazine < data.bulletsPerMagazine && !data.outOfAmmo && data.currentMagazines > 0)
        {
            data.currentBulletsInMagazine = data.bulletsPerMagazine;
            data.currentMagazines--;
            AudioManager.Instance.PlaySound(data.reloadSound, 0.5f);
            data.reloading = true;
            data.startReloading = Time.time;
        }


    }

    protected void LoadOrReloadWhenNeedIt()
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
                AudioManager.Instance.PlaySound(data.reloadSound, 0.5f);
                data.currentBulletsInMagazine = data.bulletsPerMagazine;
                data.currentMagazines--;
                data.startReloading = Time.time;
                data.reloading = true;
            }
        }
    }

    public bool GetIfOutOffAmmo()
    {
        return data.outOfAmmo;
    }
    
    protected virtual void ActionOnEnterPowerup(){}
}

