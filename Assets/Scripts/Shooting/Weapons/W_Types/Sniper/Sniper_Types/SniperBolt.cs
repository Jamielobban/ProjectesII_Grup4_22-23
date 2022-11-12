using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBolt : Sniper
{
    public SniperBolt(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
    {        
        weaponMechanism = new Repeticion();
    }


}

//, new Repeticion()
//WeaponGenerator.Instance.SetMechanismToWeapon(ref mechanism, 0);
//data.fireRateinSec *= 0.35f;
//data.fireRateinSec /= 60f; //Aqui es dps
//data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
//data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Sniper/sniperCerrojo_effect");
//data.weaponColor = Color.blue;
//data.damageMultiplier = 1.5f;
//data.amplitudeGain = 2f;
//data.bulletsPerMagazine -= 2;
//ata.currentBulletsInMagazine = data.BulletsPerMagazine;