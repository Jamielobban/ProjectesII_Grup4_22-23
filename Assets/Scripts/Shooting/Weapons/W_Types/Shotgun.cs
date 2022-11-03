using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    protected override void CheckPowerUpShooting()
    {
        throw new System.NotImplementedException();
    }

    

    protected override void Start()
    {
        base.Start();

        myType = WeaponsTypes.SHOTGUN;
        fireRateinSec = Random.Range(225f, 275f);

    }

    protected override void Update()
    {
        base.Update();
    }
}
