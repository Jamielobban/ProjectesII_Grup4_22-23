using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSemiauto : Gun
{
    public GunSemiauto(Transform _firePoint) : base(_firePoint)
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
