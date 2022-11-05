using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAuto : Gun
{
    public GunAuto(Transform _firePoint) : base(_firePoint)
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
