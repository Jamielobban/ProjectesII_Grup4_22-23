using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponsData
{
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public Sprite weaponSprite;
    public Color weaponColor;
    public Mechanism mechanism;
    public Transform firePoint;
    public GameObject bulletTypePrefab;

    public int bulletsPerMagazine;
    public int magazines;
    public int currentBulletsInMagazine;
    public int currentMagazines;

    public float damageMultiplier;

    public float reloadTimeInSec;
    public float fireRateinSec;    
    public float startReloading;
    public float timelastPowerupEnter;
    public float timelastPowerupExit;
    public float maxTimeOnPowerup;
    public float timePassed;
    public float timeLeftPowerup;
    public float amplitudeGain;

    public bool powerActive;
    public bool reloading;
    public bool outOfAmmo;
    public bool powerupAvailable;
}
