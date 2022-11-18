using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public abstract class Weapon 
{
    
    private AudioClip nextWeapon;


    protected AudioClip powerupEmpty;
    protected AudioClip powerupMax;
    protected AudioClip powerupPressed;

    protected WeaponValues data;

    protected Mechanism weaponMechanism;
    protected Transform firePoint;
    
    
    public float timer;
    bool firstEnter = true;


    public Weapon(Transform _firePoint, WeaponValues _data)
    {        
        firePoint = _firePoint;
        data = _data;
        data.RestartValues();
        powerupPressed = Resources.Load<AudioClip>("Sounds/Powerup/powerupPressed");
        powerupEmpty = Resources.Load<AudioClip>("Sounds/Powerup/powerup0");
        powerupMax = Resources.Load<AudioClip>("Sounds/Powerup/powerupMax");
        nextWeapon = Resources.Load<AudioClip>("Sounds/NextWeapon/nextWeapon");
    }


    public virtual void Update()
    {        
        
        if (!data.powerActive.RuntimeValue)
        {
            if(data.powerupAvailable.RuntimeValue && firstEnter)
            {
                AudioManager.Instance.PlaySound(powerupMax);
                firstEnter = false;
            }
            data.timePassed.RuntimeValue = Time.time - data.timelastPowerupExit.RuntimeValue;
        }        

        if (data.powerActive.RuntimeValue)
        {
            data.timeLeftPowerup.RuntimeValue = data.maxTimeOnPowerup.RuntimeValue - (Time.time - data.timelastPowerupEnter.RuntimeValue);
            firstEnter = true;
        }
        
        CheckShooting();

        InputsUpdate();

        LogicUpdate();

    }
    public float GetReloadTimeInSec()
    {
        return data.reloadTimeInSec.RuntimeValue;
    }
    public int GetBulletsInMagazine()
    {
        return data.bulletsInMagazine.RuntimeValue;
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
        return data.bulletsInMagazine.InitialValue;
    }

    public int GetCurrentMagazines()
    {
        return data.magazinesInWeapon.RuntimeValue;
    }
    public float SetTimeLeftPowerup()
    {
        return data.maxTimeOnPowerup.RuntimeValue;
    }
    public float GetTimeLeftPowerup()
    {
        if (!data.powerActive.RuntimeValue)
        {
            return 0;
        }
        return data.timeLeftPowerup.RuntimeValue;
    }
    public float GetTime()
    {
        if (data.powerActive.RuntimeValue)
        {
            return 0;
        }
        return data.timePassed.RuntimeValue;
    }
    public bool GetState()
    {
        return data.powerActive.RuntimeValue;
    }

    public bool GetReloadingState()
    {
        return data.reloading.RuntimeValue;
    }
    public void SetTime(float timePassed)
    {
        data.timelastPowerupEnter.RuntimeValue = Time.time;
        data.timelastPowerupExit.RuntimeValue = Time.time;
        data.timelastPowerupExit.RuntimeValue -= timePassed;
        //data.timelastPowerupEnter -= timePassed;
    }
    //
    private void CheckShooting()
    {
        
        if (!data.outOfAmmo.RuntimeValue && !data.reloading.RuntimeValue)
        {
            if (!data.powerActive.RuntimeValue)
            {                
                if (weaponMechanism.Shoot(data.bulletTypePrefab, firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue))
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
        if (data.reloading.RuntimeValue && Time.time - data.startReloading.RuntimeValue >= data.reloadTimeInSec.RuntimeValue +0.5f)
        {           
            data.reloading.RuntimeValue = false;
        }

        if (Time.time - data.timelastPowerupExit.RuntimeValue >= 20 && !data.powerupAvailable.RuntimeValue)
        {
            data.powerupAvailable.RuntimeValue = true;            
        }       
    }

    

    private void InputsUpdate()
    {  

        if (Input.GetButtonDown("UsePowerup"))
        {
            if (!data.powerActive.RuntimeValue && data.powerupAvailable.RuntimeValue)
            {
                data.powerActive.RuntimeValue = true;
                ActionOnEnterPowerup();
                data.timelastPowerupEnter.RuntimeValue = Time.time;     
                AudioManager.Instance.PlaySound(powerupPressed);
            }

        }
        else if (Input.GetButtonDown("Reload") && data.bulletsInMagazine.RuntimeValue < data.bulletsInMagazine.InitialValue && !data.outOfAmmo.RuntimeValue && data.magazinesInWeapon.RuntimeValue > 0)
        {
            if (data.magazinesInWeapon.RuntimeValue > 0 && data.bulletsInMagazine.RuntimeValue > 0)
            {
                data.bulletsInMagazine.RuntimeValue = data.bulletsInMagazine.InitialValue;
                data.magazinesInWeapon.RuntimeValue--;
                AudioManager.Instance.PlaySound(data.reloadSound, 0.5f);
                data.reloading.RuntimeValue = true;
                data.startReloading.RuntimeValue = Time.time;
            }
           
        }
        else if (Input.GetButtonDown("PassWeapon"))
        {
            data.outOfAmmo.RuntimeValue = true;
            data.timePassed.RuntimeValue = 0;
        }


    }

    protected void LoadOrReloadWhenNeedIt()
    {
        data.bulletsInMagazine.RuntimeValue--;
        //cinemachineShake.Instance.ShakeCamera(5f, .1f);
        if (data.bulletsInMagazine.RuntimeValue == 0)
        {
            if(data.magazinesInWeapon.RuntimeValue == 0)
            {
                data.outOfAmmo.RuntimeValue = true;
            }
            else
            {
                AudioManager.Instance.PlaySound(data.reloadSound, 0.5f);
                data.bulletsInMagazine.RuntimeValue = data.bulletsInMagazine.InitialValue;
                data.magazinesInWeapon.RuntimeValue--;
                data.startReloading.RuntimeValue = Time.time;
                data.reloading.RuntimeValue = true;                
            }
        }
    }

    public bool GetIfOutOffAmmo()
    {
        if (data.outOfAmmo.RuntimeValue)
            AudioManager.Instance.PlaySound(nextWeapon);
        return data.outOfAmmo.RuntimeValue;
    }
    
    protected virtual void ActionOnEnterPowerup(){
    }
}

