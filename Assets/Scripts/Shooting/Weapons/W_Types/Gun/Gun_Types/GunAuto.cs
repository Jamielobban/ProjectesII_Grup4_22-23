using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class GunAuto : Gun
//{
    
//    public GunAuto(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
//    {
//        //, new Repeticion()
//        //WeaponGenerator.Instance.SetMechanismToWeapon(ref mechanism, 0);
//        weaponMechanism = new Automatica();
//        //data.fireRateinSec *= 0.8f;
//        //data.fireRateinSec /= 60f; //Aqui es dps
//        //data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
//        //data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Pistol/gunAuto");
//        //data.weaponColor = Color.red;
//        //data.damageMultiplier = 1.25f;
//        temporalMechanism = new Automatica();
//        //data.amplitudeGain = 1.1f;
//    }

//    protected override bool CheckPowerUpShooting()
//    {
//        base.CheckPowerUpShooting();

//        return temporalMechanism.Shoot(data.bulletTypePrefab, secondHandClone.GetComponent<LeftHand>().firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue);
        
//    }
//}
