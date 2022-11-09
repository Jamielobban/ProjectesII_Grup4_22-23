using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public Shotgun(Transform _firePoint) : base(_firePoint)
    {
        data.bulletsPerMagazine = Random.Range(8, 15);
        data.magazines = Random.Range(1, 2); // 2/3
        data.reloadTimeInSec = 2f;
        data.maxTimeOnPowerup = 8f;
        data.currentBulletsInMagazine = data.bulletsPerMagazine;
        data.currentMagazines = data.magazines;
        data.fireRateinSec = Random.Range(225, 275); //Aqui esta en dpm
        data.weaponSprite = Resources.Load<Sprite>("Sprites/Shotgun");
        data.reloadSound = Resources.Load<AudioClip>("Sounds/Weapons/Shotgun/ShotgunReload");
        data.bulletTypePrefab = Resources.Load<GameObject>("Prefab/ShotgunBullet");


    }
    protected override void CheckPowerUpShooting()
    {
        data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = true;
        if (data.mechanism.Shoot(data.bulletTypePrefab, data.firePoint, data.fireRateinSec, data.shootSound, data.amplitudeGain))
        {
            base.LoadOrReloadWhenNeedIt();
        }
    }

    public override void Update()
    {
        base.Update();

        if (!data.powerActive)
        {
            data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
        }

        if (Time.time - data.timelastPowerupEnter >= data.maxTimeOnPowerup && data.powerActive)
        {
            data.powerActive = false;      
            data.powerupAvailable = false;
            data.timelastPowerupExit = Time.time;
            AudioManager.Instance.PlaySound(powerupEmpty);
        }
    }

}
