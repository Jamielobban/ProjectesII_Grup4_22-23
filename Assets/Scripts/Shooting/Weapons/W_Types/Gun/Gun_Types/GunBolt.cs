//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GunBolt : Gun
//{
//    public GunBolt(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
//    {
//        weaponMechanism = new Repeticion();
//        //data.fireRateinSec *= 0.09f;
//        //data.fireRateinSec /= 60f; //Aqui es dps
//        //data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
//        //data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Pistol/cerrojoPistol_effect");
//        //data.weaponColor = Color.blue;
//        //data.damageMultiplier = 4.5f;
//        temporalMechanism = new Repeticion();
//        //data.amplitudeGain = 1.5f;
//        //data.bulletsPerMagazine -= 5;
//        //data.currentBulletsInMagazine = data.bulletsPerMagazine;
//    }

//    protected override bool CheckPowerUpShooting()
//    {
//        base.CheckPowerUpShooting();

//        return temporalMechanism.Shoot(data.bulletTypePrefab, secondHandClone.GetComponent<LeftHand>().firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue);
//    }

//}
