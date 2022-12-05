using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.ParticleSystemJobs;

public class FlameThrowerBullet : Bullet
{ 

    protected override void Start()
    {
        timeShooted = Time.time;

        bulletData.bulletDamage *= bulletData._damageMultiplier;
        bulletData.bulletRangeInMetres = FindObjectOfType<RightHand>().GetWeaponInHand().GetFireRate();

        //bulletDamage = 0.5f * _damageMultiplier;
        //bulletRangeInMetres = FindObjectOfType<RightHand>().GetWeaponInHand().GetFireRate();
        //bulletSpeedMetresPerSec = 1;
        //bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        

    }

    protected override void Update()
    {
        //base.Update();
        //timeShooted = Time.time;        
        
    }
    public float GetFlamesDamage()
    {
        return bulletData.bulletDamage;
    }
}
