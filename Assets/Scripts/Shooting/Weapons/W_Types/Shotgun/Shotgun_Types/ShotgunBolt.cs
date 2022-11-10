using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBolt : Shotgun
{
    public ShotgunBolt(Transform _firePoint) : base(_firePoint)
    {
        data.mechanism = new Repeticion();
        data.fireRateinSec *= 0.2f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Shotgun/shotgunCerrojo");
        data.weaponColor = Color.blue;
        data.damageMultiplier = 1.5f;
        data.amplitudeGain = 1.7f;
        data.bulletsPerMagazine -= 4;
        data.currentBulletsInMagazine = data.bulletsPerMagazine;
    }    
    protected override void CheckPowerUpShooting()
    {
        base.CheckPowerUpShooting();
    }
}
