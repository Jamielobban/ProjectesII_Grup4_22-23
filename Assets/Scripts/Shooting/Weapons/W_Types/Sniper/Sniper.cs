using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sniper : Weapon
{
    
    public Sniper(Transform _firePoint) :base(_firePoint) {   
        data.bulletsPerMagazine = Random.Range(4,9);
        data.magazines = Random.Range(2, 4);       
        data.reloadTimeInSec = 3f;
        data.maxTimeOnPowerup = 5;
        data.currentBulletsInMagazine = data.bulletsPerMagazine;
        data.currentMagazines = data.magazines;
        data.fireRateinSec = Random.Range(100f, 200f); //Aqui esta en dpm
        data.weaponSprite = Resources.Load<Sprite>("Sprites/Sniper");        
        data.reloadSound = Resources.Load<AudioClip>("Sounds/Weapons/Sniper/Reload");
        data.bulletTypePrefab = Resources.Load<GameObject>("Prefab/SniperBullet");
        //Debug.Log(data.bulletTypePrefab);
    }

    protected override void CheckPowerUpShooting()
    {        
        data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = true;
        if(data.mechanism.Shoot(data.bulletTypePrefab, data.firePoint, data.fireRateinSec, data.shootSound, data.amplitudeGain, data.damageMultiplier))
        {
            base.LoadOrReloadWhenNeedIt();
        }
    }

    public override void Update()
    {
        base.Update();

        if(Time.time - data.timelastPowerupEnter >= data.maxTimeOnPowerup && data.powerActive)
        {
            data.powerActive = false;
            data.powerupAvailable = false;
            data.fireRateinSec /= 0.5f;
            data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
            data.timelastPowerupExit = Time.time;
            AudioManager.Instance.PlaySound(powerupEmpty);
        }
        if (!data.powerActive)
        {
            data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
        }
    }

    protected override void ActionOnEnterPowerup()
    {
        data.fireRateinSec *= 0.5f;
    }

}
 