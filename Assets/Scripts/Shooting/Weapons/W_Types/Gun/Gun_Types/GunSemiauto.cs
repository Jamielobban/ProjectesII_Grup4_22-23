using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSemiauto : Gun
{
    public GunSemiauto(Transform _firePoint) : base(_firePoint)
    {
        data.mechanism = new Seamiautomatica();
        data.fireRateinSec *= 1.02f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Pistol/semiAutomaticPistol_effect"); 
        data.weaponColor = Color.yellow;
        temporalMechanism = new Seamiautomatica();
    }

    protected override void CheckPowerUpShooting()
    {
        base.CheckPowerUpShooting();

        temporalMechanism.Shoot(data.bulletTypePrefab, secondHandClone.GetComponent<LeftHand>().firePoint, data.fireRateinSec, data.shootSound, data.amplitudeGain);
    }
}
