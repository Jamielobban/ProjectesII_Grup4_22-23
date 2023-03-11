using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponValues : ScriptableObject
{
    public string WeaponName;
    public MechanismTypes mechanismType;

    [Header("Parametrizable variables")]
    public AudioClip shootSound;    
    public AudioClip reloadSound;    
    public Sprite weaponSprite;    
    public Color weaponColor;      
    public GameObject bulletTypePrefab;    
    public IntValue bulletsInMagazine;    
    public IntValue magazinesInWeapon;    
    public FloatValue damageMultiplier;    
    public FloatValue reloadTimeInSec;    
    public FloatValue fireRateinSec;    
    public FloatValue maxTimeOnPowerup;
    public FloatValue amplitudeGain;
    public FloatValue knockbackForce;
    public FloatValue knockbackMinimum;

    [Header("Default InGame variables")]
    public FloatValue startReloading;    
    public FloatValue timelastPowerupEnter;    
    public FloatValue timelastPowerupExit;    
    public FloatValue timePassed;    
    public FloatValue timeLeftPowerup;    
    public BoolValue powerActive;   
    public BoolValue reloading;    
    public BoolValue outOfAmmo;    
    public BoolValue powerupAvailable;



    [Header("Firepoint")]
    public Vector3 firePoint;

    [Header("UI")]
    public Sprite fullAmmo, emptyAmmo, flashAmmo;

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt(WeaponName + "balas", bulletsInMagazine.RuntimeValue + (bulletsInMagazine.InitialValue* (magazinesInWeapon.RuntimeValue-1)));
    }

    private void Awake()
    {
        GetPlayerPrefs();
    }

    public void GetPlayerPrefs()
    {
        magazinesInWeapon.RuntimeValue = (int)((PlayerPrefs.GetInt(WeaponName + "balas") / bulletsInMagazine.InitialValue)+1);
        bulletsInMagazine.RuntimeValue = bulletsInMagazine.InitialValue%PlayerPrefs.GetInt(WeaponName + "balas");


    }
    public void RestartValues()
    {
        bulletsInMagazine.RestartValues();
        magazinesInWeapon.RestartValues();
        damageMultiplier.RestartValues();
        reloadTimeInSec.RestartValues();
        fireRateinSec.RestartValues();
        maxTimeOnPowerup.RestartValues();
        amplitudeGain.RestartValues();
        //knockbackForce.RestartValues();
        //knockbackMinimum.RestartValues();
        startReloading.RestartValues();
        timelastPowerupEnter.RestartValues();
        timelastPowerupExit.RestartValues();
        timePassed.RestartValues();
        timeLeftPowerup.RestartValues();
        powerActive.RestartValues();
        reloading.RestartValues();
        outOfAmmo.RestartValues();
        powerupAvailable.RestartValues();
    }

    

}
