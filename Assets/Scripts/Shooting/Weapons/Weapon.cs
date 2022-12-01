using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Threading.Tasks;
using System;


public abstract class Weapon 
{
    
    private AudioClip nextWeapon;


    protected AudioClip powerupEmpty;
    protected AudioClip powerupMax;
    protected AudioClip powerupPressed;

    protected WeaponValues data;

    protected Mechanism weaponMechanism;
    protected Transform firePoint;

    public float powerupRechargeTime = 20;
    public float timer;
    bool firstEnter = true;
    bool reloading = false;

    public Weapon(Transform _firePoint, WeaponValues _data)
    {        
        firePoint = _firePoint;
        data = _data;
        data.RestartValues();
        powerupPressed = Resources.Load<AudioClip>("Sounds/Powerup/powerupPressed");
        powerupEmpty = Resources.Load<AudioClip>("Sounds/Powerup/powerup0");
        powerupMax = Resources.Load<AudioClip>("Sounds/Powerup/powerupMax");
        nextWeapon = Resources.Load<AudioClip>("Sounds/NextWeapon/nextWeapon");
        PowerupCharge();
    }


    public virtual void Update()
    {

        //if (!data.powerActive.RuntimeValue)
        //{
        //    if(data.powerupAvailable.RuntimeValue && firstEnter)
        //    {
        //        AudioManager.Instance.PlaySound(powerupMax, GameObject.FindGameObjectWithTag("Player").transform);
        //        firstEnter = false;
        //    }
        //    data.timePassed.RuntimeValue = Time.time - data.timelastPowerupExit.RuntimeValue;
        //}        

        //if (data.powerActive.RuntimeValue)
        //{
        //    data.timeLeftPowerup.RuntimeValue = data.maxTimeOnPowerup.RuntimeValue - (Time.time - data.timelastPowerupEnter.RuntimeValue);
        //    firstEnter = true;
        //}        

        //InputsUpdate();

        //LogicUpdate();

        LogicUpdate();

    }

    private void LogicUpdate()
    {
        if (data.powerActive.RuntimeValue)
        {
            if (data.bulletsInMagazinePowerup.RuntimeValue <= 0 && !data.reloading.RuntimeValue)
            {
                data.reloading.RuntimeValue = true;
                Reloading(data.reloadTimeInSecPowerup.InitialValue, data.bulletsInMagazinePowerup);
            }
        }
        else
        {
            if (data.bulletsInMagazine.RuntimeValue <= 0 && !data.reloading.RuntimeValue)
            {
                data.reloading.RuntimeValue = true;
                Reloading(data.reloadTimeInSec.InitialValue, data.bulletsInMagazine);
            }
        }
        
    }


    private void InputsUpdate()
    {

        if (Input.GetButtonDown("UsePowerup"))
        {
            if (!data.powerActive.RuntimeValue && data.powerupAvailable.RuntimeValue)
            {
                data.powerActive.RuntimeValue = true;
                PowerupEnding();                
            }

        }
        else if (Input.GetButtonDown("Reload"))// && data.bulletsInMagazine.RuntimeValue < data.bulletsInMagazine.InitialValue && !data.outOfAmmo.RuntimeValue && data.magazinesInWeapon.RuntimeValue > 0 && !data.reloading.RuntimeValue)
        {
            if (data.powerActive)
            {
                if(data.bulletsInMagazinePowerup.InitialValue > data.bulletsInMagazine.RuntimeValue && !data.reloading.RuntimeValue)
                {
                    data.reloading.RuntimeValue = true;
                    Reloading(data.reloadTimeInSecPowerup.InitialValue, data.bulletsInMagazinePowerup);
                }
            }
            else
            {
                if (data.bulletsInMagazinePowerup.InitialValue > data.bulletsInMagazine.RuntimeValue && !data.reloading.RuntimeValue && data.magazinesInWeapon.RuntimeValue > 0)
                {
                    data.reloading.RuntimeValue = true;
                    Reloading(data.reloadTimeInSec.InitialValue, data.bulletsInMagazine);
                }
            }            

        }
        else if (Input.GetButtonDown("PassWeapon"))
        {
            data.outOfAmmo.RuntimeValue = true;
            //data.timePassed.RuntimeValue = 0;
        }


    }

    private async void Reloading(float reloadWaitTime, IntValue magazineReloaded)
    {
        await Task.Delay(TimeSpan.FromSeconds(reloadWaitTime));
        data.reloading.RuntimeValue = false;
        magazineReloaded.RestartValues();
    }

    private async void PowerupCharge()
    {

        await Task.Delay(TimeSpan.FromSeconds(powerupRechargeTime));
        data.powerupAvailable.RuntimeValue = true;
        data.bulletsInMagazinePowerup.RestartValues();
    }

    private async void PowerupEnding()
    {
        await Task.Delay(TimeSpan.FromSeconds(data.maxTimeOnPowerup.InitialValue));
        data.powerActive.RuntimeValue = false;
        PowerupCharge();
    }

    public virtual void FixedUpdate()
    {
        CheckShooting();
    }
    //public float GetReloadTimeInSec()
    //{
    //    return data.reloadTimeInSec.RuntimeValue;
    //}
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
    //public float GetTimeLeftPowerup()
    //{
    //    if (!data.powerActive.RuntimeValue)
    //    {
    //        return 0;
    //    }
    //    return data.timeLeftPowerup.RuntimeValue;
    //}
    //public float GetTime()
    //{
    //    if (data.powerActive.RuntimeValue)
    //    {
    //        return 0;
    //    }
    //    return data.timePassed.RuntimeValue;
    //}
    public bool GetState()
    {
        return data.powerActive.RuntimeValue;
    }

    public bool GetReloadingState()
    {
        return data.reloading.RuntimeValue;
    }
    //public void SetTime(float timePassed)
    //{
    //    data.timelastPowerupEnter.RuntimeValue = Time.time;
    //    data.timelastPowerupExit.RuntimeValue = Time.time;
    //    data.timelastPowerupExit.RuntimeValue -= timePassed;
    //    //data.timelastPowerupEnter -= timePassed;
    //}
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
                AudioManager.Instance.PlaySoundDelayed(data.reloadSound, 0.5f, GameObject.FindGameObjectWithTag("Player").transform);
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
            AudioManager.Instance.PlaySound(nextWeapon, GameObject.FindGameObjectWithTag("Player").transform);
        return data.outOfAmmo.RuntimeValue;
    }
    
    protected virtual void ActionOnEnterPowerup(){
    }

    public float GetFireRate()
    {
        return data.fireRateinSec.InitialValue;
    }
}

