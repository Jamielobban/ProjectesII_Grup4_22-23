using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBolt : Gun
{
    public GunBolt(Transform _firePoint) : base(_firePoint)
    {
        data.mechanism = new Repeticion();
        data.fireRateinSec *= 0.09f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Pistol/cerrojoPistol_effect");
        data.weaponColor = Color.blue;
        temporalMechanism = new Repeticion();
    }

    protected override void CheckPowerUpShooting()
    {
        base.CheckPowerUpShooting();

        temporalMechanism.Shoot(data.bulletTypePrefab, secondHandClone.GetComponent<LeftHand>().firePoint, data.fireRateinSec, data.shootSound, data.amplitudeGain);
    }

}
