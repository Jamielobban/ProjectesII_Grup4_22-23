using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperSemi : Sniper { 

    public SniperSemi(Transform _firePoint) : base(_firePoint)
    {
        //, new Repeticion()
        //WeaponGenerator.Instance.SetMechanismToWeapon(ref mechanism, 0);
        data.mechanism = new Seamiautomatica();
        data.fireRateinSec *= 0.9f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Sniper/sniperSemiAutomatic_effect");
        data.weaponColor = Color.yellow;

    }
}
