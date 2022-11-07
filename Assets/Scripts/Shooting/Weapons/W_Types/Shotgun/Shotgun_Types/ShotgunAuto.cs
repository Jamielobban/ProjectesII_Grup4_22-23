using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAuto : Shotgun
{
    public ShotgunAuto(Transform _firePoint) : base(_firePoint)
    {
        data.mechanism = new Automatica();
        data.fireRateinSec *= 1.1f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Pistol/gunAuto");
        data.weaponColor = Color.red;
        
    }   
    protected override void CheckPowerUpShooting()
    {
        base.CheckPowerUpShooting();
    }
    
}
