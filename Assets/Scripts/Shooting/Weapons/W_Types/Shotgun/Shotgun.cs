using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public Shotgun(Transform _firePoint, ref SpriteRenderer _sr) : base(_firePoint, ref _sr)
    {

    }
    protected override void CheckPowerUpShooting()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        base.Update();
    }

}
