using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBolt : Sniper
{
    public SniperBolt(Transform _firePoint, ref SpriteRenderer _sr) : base(_firePoint, ref _sr)
    {
        //, new Repeticion()
        //WeaponGenerator.Instance.SetMechanismToWeapon(ref mechanism, 0);
        data.mechanism = new Repeticion();
        data.fireRateinSec *= 0.5f;
        data.fireRateinSec /= 60f; //Aqui es dps
        data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
        _sr.color = Color.blue;
    }


}
