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
        reloadTimeInSec = 3.8f;
        fireRateinSec = 2.0f;
        hasBoltSound = true;

        currentBulletsInMagazine = bulletsPerMagazine;
        currentMagazines = magazines;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    
}
 