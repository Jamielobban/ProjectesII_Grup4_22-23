using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mechanism
{    
    protected float timeLastShoot;
    public bool isDoubleHand = false;    

    public Mechanism()
    {
        timeLastShoot = 0;
    }
    
    public abstract bool Shoot(GameObject bulletTypePrefab, Transform firePoint, float fireRateinSec, AudioClip shootSound);
    

    public abstract float GetFireRateMultiplier(float min, float max);

}
  