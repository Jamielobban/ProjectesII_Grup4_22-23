using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    protected override void CheckPowerUpShooting()
    {
        throw new System.NotImplementedException();
    }
    

    protected override void Start()
    {
        base.Start();

        myType = WeaponsTypes.GUN;
        fireRateinSec = Random.Range(660f, 700f);

    }

    protected override void Update()
    {
        base.Update();
    }
}
