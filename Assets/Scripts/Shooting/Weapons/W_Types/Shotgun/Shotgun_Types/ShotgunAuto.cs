using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunAuto : Shotgun
{
    public ShotgunAuto(Transform _firePoint) : base(_firePoint)
    {

    }
    public override void Update()
    {
        base.Update();
    }

    protected override void ActionOnEnterPowerup()
    {
        base.ActionOnEnterPowerup();
    }

    protected override void CheckPowerUpShooting()
    {
        base.CheckPowerUpShooting();
    }
    
}
