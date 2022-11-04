using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mechanism : MonoBehaviour
{    
    protected float timeLastShoot;
    public bool isDoubleHand = false;
    public AudioClip shootSound, boltSound, reloadSound;

    private void Start()
    {
        timeLastShoot = 0;
    }
    public abstract bool Shoot(GameObject bulletTypePrefab, Transform firePoint, float fireRateinSec);
    

    public abstract float GetFireRateMultiplier(float min, float max);

}
  