using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{
    private Rigidbody2D rb;   
    [SerializeField]
    private GameObject childForCollisions;
        

    protected override void Start()
    {
        base.Start();

        bulletDamage = 80*_damageMultiplier;
        bulletRangeInMetres = 150;
        bulletSpeedMetresPerSec = 30;
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

        if (!powerUpOn)
        {
            childForCollisions.SetActive(false);            
        }
        else
        {
            childForCollisions.SetActive(true);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit"))
        {
            if (!powerUpOn)
                base.ImpactWall();
            else
            {
                base.HitSomething();
            }


        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            base.HitSomeone();
            bulletInfo.damage = bulletDamage;
            bulletInfo.impactPosition = transform.position;
            collision.gameObject.SendMessage("GetDamage", bulletInfo);
        }
    }  
    

    

   
}
