using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.ParticleSystemJobs;

public class FlameThrowerBullet : Bullet
{
    private Rigidbody2D rb;
    
    

    protected override void Start()
    {
        timeShooted = Time.time;     

        bulletDamage = 0.5f * _damageMultiplier;
        bulletRangeInMetres = FindObjectOfType<RightHand>().GetWeaponInHand().GetFireRate();
        bulletSpeedMetresPerSec = 1;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        Transform originalFirePoint = this.transform;

    }

    protected override void Update()
    {
        //base.Update();
        //timeShooted = Time.time;        
        
    }
    public float GetFlamesDamage()
    {
        return bulletDamage;
    }
}
