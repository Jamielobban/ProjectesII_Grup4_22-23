using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAuto : Sniper
{
    public SniperAuto(Transform _firePoint) : base(_firePoint)
    {
        //, new Repeticion()
        //WeaponGenerator.Instance.SetMechanismToWeapon(ref mechanism, 0);
        data.mechanism = new Automatica();
        data.fireRateinSec *= 1.5f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Sniper/sniperAutomatic_effect");
        data.weaponColor = Color.red;
    }
}
