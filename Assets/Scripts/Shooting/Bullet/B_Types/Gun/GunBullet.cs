using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Bullet
{
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();

        bulletDamage = 50;
        bulletRangeInMetres = 100;
        bulletSpeedMetresPerSec = 20;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        Transform originalFirePoint = this.transform;
        rb.AddForce(originalFirePoint.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);
    }

    protected override void Update()
    {
        base.Update();

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
               
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit"))
        {
            base.Impact();            
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            base.Impact();
        }
    }


    private void HitSomeone()
    {
        Instantiate(collisionEffect, transform.position, Quaternion.identity);
    }
}
