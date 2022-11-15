using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Bullet
{
    private Rigidbody2D rb;    

    protected override void Start()
    {
        base.Start();

        bulletDamage = 20*_damageMultiplier;
        bulletRangeInMetres = 100;
        bulletSpeedMetresPerSec = 20;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        Transform originalFirePoint = this.transform;
        rb.AddForce(originalFirePoint.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);
    }
    private void FixedUpdate()
    {
        Vector2 normalizedVel = rb.velocity.normalized;
        float waveVariation1 = 0.8f * Mathf.Sin(Time.time * rb.velocity.magnitude);
        float waveVariation2 = 0.8f * Mathf.Sin(Time.time * rb.velocity.magnitude + Mathf.PI);
        transform.position = new Vector3(transform.position.x + normalizedVel.y * waveVariation2, transform.position.y + normalizedVel.x * waveVariation1, transform.position.z);
    }
    protected override void Update()
    {
        base.Update();




        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
        //transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
               
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit"))
        {
            base.ImpactWall();            
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            base.ImpactBody();
            bulletInfo.damage = bulletDamage;
            bulletInfo.impactPosition = transform.position;
            collision.gameObject.SendMessage("GetDamage", bulletInfo);
        }
    }


    //private void HitSomeone()
    //{
    //    Instantiate(collisionEffect, transform.position, Quaternion.identity);
    //}
}
