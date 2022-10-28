using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        bulletsPerMagazine = 9;
        magazines = 3;
        reloadTimeInSec = 2.1f;
        fireRateinSec = 1.2f;

        currentBulletsInMagazine = bulletsPerMagazine;
        currentMagazines = magazines;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    
}
 