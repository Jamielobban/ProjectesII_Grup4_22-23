using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


//public enum WeaponsTypes { SNIPER, GUN, SHOTGUN, UNKNOWN };


public abstract class Weapon /*: MonoBehaviour*/
{    
    protected WeaponsData data;
    public float timer;
    protected AudioClip powerupEmpty;
    protected AudioClip powerupMax;
    protected AudioClip powerupPressed;
    AudioClip nextWeapon;
    bool firstEnter = true;
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
        powerupPressed = Resources.Load<AudioClip>("Sounds/Powerup/powerupPressed");
        powerupEmpty = Resources.Load<AudioClip>("Sounds/Powerup/powerup0");
        powerupMax = Resources.Load<AudioClip>("Sounds/Powerup/powerupMax");
        nextWeapon = Resources.Load<AudioClip>("Sounds/NextWeapon/nextWeapon");
    }


    public virtual void Update()
    {
        //Debug.Log(data.timePassed);
        
        if (!data.powerActive)
        {
            if(data.powerupAvailable && firstEnter)
            {
                AudioManager.Instance.PlaySound(powerupMax);
                firstEnter = false;
            }
            data.timePassed = Time.time - data.timelastPowerupExit;
        }
        
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
            firstEnter = true;
        }
        
        CheckShooting();

        InputsUpdate();

        LogicUpdate();

    }
    public float GetReloadTimeInSec()
    {
        return data.reloadTimeInSec;
    }
    public int GetBulletsInMagazine()
    {
        return data.currentBulletsInMagazine;
    }

    public Sprite GetSprite()
    {
        return data.weaponSprite;
    }

    public Color GetWeaponColor()
    {
        return data.weaponColor;
    }

    public int GetBulletsPerMagazine()
    {
        return data.bulletsPerMagazine;
    }

    public int GetCurrentMagazines()
    {
        return data.currentMagazines;
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

    public bool GetReloadingState()
    {
        return data.reloading;
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
                if (data.mechanism.Shoot(data.bulletTypePrefab, data.firePoint, data.fireRateinSec, data.shootSound, data.amplitudeGain, data.damageMultiplier))
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
                AudioManager.Instance.PlaySound(powerupPressed);
            }

        }
        else if (Input.GetButtonDown("Reload") && data.currentBulletsInMagazine < data.bulletsPerMagazine && !data.outOfAmmo && data.currentMagazines > 0)
        {
            if (data.currentMagazines > 0 && data.currentBulletsInMagazine > 0)
            {
                data.currentBulletsInMagazine = data.bulletsPerMagazine;
                data.currentMagazines--;
                AudioManager.Instance.PlaySound(data.reloadSound, 0.5f);
                data.reloading = true;
                data.startReloading = Time.time;
            }
           
        }
        else if (Input.GetButtonDown("PassWeapon"))
        {
            data.outOfAmmo = true;
            data.timePassed = 0;
        }


    }

    protected void LoadOrReloadWhenNeedIt()
    {
        data.currentBulletsInMagazine--;
        //cinemachineShake.Instance.ShakeCamera(5f, .1f);
        if (data.currentBulletsInMagazine == 0)
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
        if (data.outOfAmmo)
            AudioManager.Instance.PlaySound(nextWeapon);
        return data.outOfAmmo;
    }
    
    protected virtual void ActionOnEnterPowerup(){
    }
}

