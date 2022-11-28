using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrowerBullet : Bullet
{
    private Rigidbody2D rb;
    

    void Start()
    {
        bulletDamage = 10 * _damageMultiplier;
        bulletRangeInMetres = FindObjectOfType<RightHand>().GetWeaponInHand().GetFireRate();
        bulletSpeedMetresPerSec = 1;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        Transform originalFirePoint = this.transform;
        
    }
        
}
