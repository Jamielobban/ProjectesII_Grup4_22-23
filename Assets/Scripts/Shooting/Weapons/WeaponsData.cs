using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponsData
{
    public AudioClip shootSound;
    public AudioClip reloadSound;
    public Sprite weaponSprite;
    public Mechanism mechanism;
    public Transform firePoint;
    public GameObject bulletTypePrefab;

    public int bulletsPerMagazine;
    public int magazines;
    public int currentBulletsInMagazine;
    public int currentMagazines;

    public float reloadTimeInSec;
    public float fireRateinSec;
    public float startReloading;
    public float timelastPowerupUse;
    
    public bool powerActive;
    public bool reloading;
    public bool outOfAmmo;
    public bool powerupAvailable;
}
