using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public enum WeaponsTypes { SNIPER, GUN, SHOTGUN, UNKNOWN };


public abstract class Weapon /*: MonoBehaviour*/
{    
    protected WeaponsData data;
    public float timer;
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

    private WeaponUI weaponUI;
    
    public virtual void Update()
    {

        if (data.powerupAvailable)
        {
            //Debug.Log("available");
            
        }

        Debug.Log(data.timePassed);
        //Debug.Log(data.timePassed);
        //if (data.timelastPowerupExit == 0)
        //{
        //    data.timelastPowerupEnter = Time.time;
        //    data.timelastPowerupExit = Time.time;
        //}
        if (!data.powerActive)
        {
            data.timePassed = Time.time - data.timelastPowerupExit;
        }
        //else
        //{
        //    data.timePassed = 0;
        //}

        //Debug.Log(data.currentBulletsInMagazine);
        //Debug.Log(data.bulletsPerMagazine);
        //Debug.Log(data.currentMagazines);
        //Debug.Log(data.fireRateinSec);
        //Debug.Log(data.reloadTimeInSec);
        //Debug.Log(data.timeLeftPowerup);
        //Debug.Log(data.timePassed);



        if (data.powerActive)
        {
            data.timeLeftPowerup = data.maxTimeOnPowerup - (Time.time - data.timelastPowerupEnter);
            //Debug.Log(data.timeLeftPowerup);

        }

        //Debug.Log(data.timePassed);
        CheckShooting();

        InputsUpdate();

        LogicUpdate();

    }

    public float SetTimeLeftPowerup()
    {
        return data.maxTimeOnPowerup;
    }
    public float GetTimeLeftPowerup()
    {
        if (!data.powerActive)
        {
            return 0;
        }
        return data.timeLeftPowerup;
    }
    public float GetTime()
    {
        if (data.powerActive)
        {
            return 0;
        }
        return data.timePassed;
    }
    public bool GetState()
    {
        return data.powerActive;
    }

    public void SetTime(float timePassed)
    {
        data.timelastPowerupEnter = Time.time;
        data.timelastPowerupExit = Time.time;
        data.timelastPowerupExit -= timePassed;
        //data.timelastPowerupEnter -= timePassed;
    }
    //
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

        if (Time.time - data.timelastPowerupExit >= 20 && !data.powerupAvailable)
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

