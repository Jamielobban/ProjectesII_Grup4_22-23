//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Shotgun : Weapon
//{
//    int? powerupEmptyKey;
//    public Shotgun(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
//    {
//        //data.bulletsPerMagazine = Random.Range(8, 15);
//        //data.magazines = Random.Range(1, 2); // 2/3
//        //data.reloadTimeInSec = 2f;
//        //data.maxTimeOnPowerup = 8f;
//        //data.currentBulletsInMagazine = data.BulletsPerMagazine;
//        //data.currentMagazines = data.Magazines;
//        //data.fireRateinSec = Random.Range(225, 275); //Aqui esta en dpm
//        //data.weaponSprite = Resources.Load<Sprite>("Sprites/Shotgun");
//        //data.reloadSound = Resources.Load<AudioClip>("Sounds/Weapons/Shotgun/ShotgunReload");
//        //data.bulletTypePrefab = Resources.Load<GameObject>("Prefab/ShotgunBullet");


//    }
//    protected override bool CheckPowerUpShooting()
//    {
//        data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = true;
//        if (weaponMechanism.Shoot(data.bulletTypePrefab, firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue))
//        {
//            base.LoadOrReloadWhenNeedIt();
//            return true;
//        }
//        return false;
//    }

//    public override void Update()
//    {
//        base.Update();

//        if (!data.powerActive.RuntimeValue)
//        {
//            data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
//        }

//        if (Time.time - data.timelastPowerupEnter.RuntimeValue >= data.maxTimeOnPowerup.RuntimeValue && data.powerActive.RuntimeValue)
//        {
//            data.powerActive.RuntimeValue = false;      
//            data.powerupAvailable.RuntimeValue = false;
//            data.timelastPowerupExit.RuntimeValue = Time.time;
//            powerupEmptyKey = AudioManager.Instance.LoadSound(powerupEmpty, player.transform);
//        }
//    }

//}
