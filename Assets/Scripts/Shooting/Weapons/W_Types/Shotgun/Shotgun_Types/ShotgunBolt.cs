using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBolt : Shotgun
{
    public ShotgunBolt(Transform _firePoint) : base(_firePoint)
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
