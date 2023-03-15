//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GunSemiauto : Gun
//{

//    //public override void Star
//    public GunSemiauto(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
//    {
//        weaponMechanism = new Seamiautomatica();
//        //data.fireRateinSec *= 1.02f;
//        //data.fireRateinSec /= 60f; //Aqui es dps
//        //data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
//        //data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Pistol/semiAutomaticPistol_effect"); 
//        //data.weaponColor = Color.yellow;
//        //data.damageMultiplier = 2;
//        temporalMechanism = new Seamiautomatica();
//        //data.amplitudeGain = 1.25f;
//    }

//    protected override bool CheckPowerUpShooting()
//    {
//        base.CheckPowerUpShooting();

//        return temporalMechanism.Shoot(data.bulletTypePrefab, secondHandClone.GetComponent<LeftHand>().firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue);
//    }
//}
