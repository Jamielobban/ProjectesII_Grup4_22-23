using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{    
    public Gun(Transform _firePoint) : base(_firePoint)
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
