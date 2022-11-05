using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBolt : Sniper
{
    public SniperBolt(Transform _firePoint) : base(_firePoint)
    {
        //, new Repeticion()
        //WeaponGenerator.Instance.SetMechanismToWeapon(ref mechanism, 0);
        data.mechanism = new Repeticion();
        data.fireRateinSec *= 0.35f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Sniper/sniperCerrojo_effect");
        data.weaponColor = Color.blue;
    }


}
