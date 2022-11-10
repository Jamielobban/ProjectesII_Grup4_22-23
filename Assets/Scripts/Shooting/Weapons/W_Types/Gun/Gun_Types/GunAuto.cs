using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAuto : Gun
{
    
    public GunAuto(Transform _firePoint) : base(_firePoint)
    {
        //, new Repeticion()
        //WeaponGenerator.Instance.SetMechanismToWeapon(ref mechanism, 0);
        data.mechanism = new Automatica();
        data.fireRateinSec *= 0.8f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Pistol/gunAuto");
        data.weaponColor = Color.red;
        data.damageMultiplier = 1.25f;
        temporalMechanism = new Automatica();
        data.amplitudeGain = 1.1f;
    }

    protected override void CheckPowerUpShooting()
    {
        base.CheckPowerUpShooting();

        temporalMechanism.Shoot(data.bulletTypePrefab, secondHandClone.GetComponent<LeftHand>().firePoint, data.fireRateinSec, data.shootSound, data.amplitudeGain, data.damageMultiplier);
        
    }
}
