using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sniper : Weapon
{
    
    public Sniper(Transform _firePoint, WeaponValues _data) :base(_firePoint, _data) {   
        //data.bulletsPerMagazine = Random.Range(6,9);
        //data.magazines = Random.Range(1, 3);       
        //data.reloadTimeInSec = 3f;
        //data.maxTimeOnPowerup = 5;
        //data.currentBulletsInMagazine = data.BulletsPerMagazine;
        //data.currentMagazines = data.Magazines;
        //data.fireRateinSec = Random.Range(100f, 200f); //Aqui esta en dpm
        //data.weaponSprite = Resources.Load<Sprite>("Sprites/Sniper");        
        //data.reloadSound = Resources.Load<AudioClip>("Sounds/Weapons/Sniper/Reload");
        //data.bulletTypePrefab = Resources.Load<GameObject>("Prefab/SniperBullet");
        //Debug.Log(data.bulletTypePrefab);
    }

    protected override bool CheckPowerUpShooting()
    {
        //data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = true;
        //if(weaponMechanism.Shoot(data.bulletTypePrefab, firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue))
        //{
        //    base.LoadOrReloadWhenNeedIt();
        //}
        return false;
    }

    public override void Update()
    {
        base.Update();

        if(Time.time - data.timelastPowerupEnter.RuntimeValue >= data.maxTimeOnPowerup.RuntimeValue && data.powerActive.RuntimeValue)
        {
            data.powerActive.RuntimeValue = false;
            data.powerupAvailable.RuntimeValue = false;
            //data.fireRateinSec /= 0.5f;
            //data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
            data.timelastPowerupExit.RuntimeValue = Time.time;
            AudioManager.Instance.PlaySound(powerupEmpty,player.transform);
        }
        if (!data.powerActive)
        {
            data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
        }
    }

    protected override void ActionOnEnterPowerup()
    {
        //data.fireRateinSec *= 0.5f;
    }

}
 