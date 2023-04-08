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

    public bool unLock;

    [Header("Firepoint")]
    public Vector3 firePoint;

    [Header("UI")]
    public Sprite fullAmmo, emptyAmmo, flashAmmo;

    public void SavePlayerPrefs()
    {
        
        PlayerPrefs.SetInt(WeaponName + "Desbloqueada", (unLock ? 1 : 0));
        PlayerPrefs.SetInt(WeaponName + "balas", bulletsInMagazine.RuntimeValue + (bulletsInMagazine.InitialValue* (magazinesInWeapon.RuntimeValue)));
    }

    public void restartWeapon()
    {
        unLock = ((PlayerPrefs.GetInt(WeaponName + "Desbloqueada",0) == 1));
        GetPlayerPrefs();       

    }
    private void Awake()
    {
        //unLock = false;
        //restartWeapon();

    }

    public void GetPlayerPrefs()
    {
        int a = 0;
         
        int cargadoresIniciales = 5;

        if (WeaponName == "Pistol")
        {
            cargadoresIniciales = 9999;
        }

        if (bulletsInMagazine != null)
        {
            a = bulletsInMagazine.InitialValue * cargadoresIniciales;

            bulletsInMagazine.RuntimeValue = PlayerPrefs.GetInt(WeaponName + "balas", a) % bulletsInMagazine.InitialValue;
        }

        if (PlayerPrefs.GetInt(WeaponName + "balas", a) <= 0)
            outOfAmmo.RuntimeValue = true;
        else
            outOfAmmo.RuntimeValue = false;

        if (bulletsInMagazine.RuntimeValue == 0 && bulletsInMagazine != null && PlayerPrefs.GetInt(WeaponName + "balas", a) > 0)
            bulletsInMagazine.RuntimeValue = bulletsInMagazine.InitialValue;

        if(magazinesInWeapon != null && bulletsInMagazine != null)
        magazinesInWeapon.RuntimeValue = (int)((PlayerPrefs.GetInt(WeaponName + "balas", a) / bulletsInMagazine.InitialValue));

        if (bulletsInMagazine.RuntimeValue == bulletsInMagazine.InitialValue && magazinesInWeapon.RuntimeValue != 0 && magazinesInWeapon != null && bulletsInMagazine != null && PlayerPrefs.GetInt(WeaponName + "balas", a) > 0)
        {
            magazinesInWeapon.RuntimeValue--;
        }

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
