using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSemiauto : Shotgun
{
    public ShotgunSemiauto(Transform _firePoint) : base(_firePoint)
    {
        data.mechanism = new Seamiautomatica();
        data.fireRateinSec *= 0.7f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Shotgun/shotgunSemi");
        data.weaponColor = Color.yellow;
        
    }    

    protected override void CheckPowerUpShooting()
    {
        base.CheckPowerUpShooting();
    }
}
