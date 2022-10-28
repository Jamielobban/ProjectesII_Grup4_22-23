using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{
   
    protected override void Start()
    {
        base.Start();

        bulletDamage = 50;
        bulletRangeInMetres = 10000;
        bulletSpeedMetresPerSec = 30;
        bulletRadius = 0.23f;

        Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
        Transform originalFirePoint = this.transform;
        rb.AddForce(originalFirePoint.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
    }

    protected override void Update()
    {
        base.Update();

        if (enemyHit)
        {
            HitSomeone();
            enemyHit = false;
        }
    }    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MapLimit"))
        {
            base.Impact();
        }
        else  if (collision.CompareTag("Enemy"))
        {
            HitSomeone();
        }        

    }

    private void HitSomeone()
    {
        Instantiate(collisionEffect, transform.position, Quaternion.identity);
    }
}
