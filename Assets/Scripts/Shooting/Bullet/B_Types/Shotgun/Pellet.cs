using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : Bullet
{       
    [SerializeField]
    SpriteRenderer sr;

    private PlayerMovement playerpos;

    protected override void Start()
    {
        base.Start();
        playerpos = FindObjectOfType<PlayerMovement>();
        //bulletDamage = 34*_damageMultiplier;
        //bulletRangeInMetres = 500;
        //bulletSpeedMetresPerSec = 20;
        //bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        bulletData.bulletDamage *= bulletData._damageMultiplier;
        playerpos.knockback = true;
        playerpos.rb.velocity = new Vector2((-rb.velocity.x * playerpos.knockbackForce) / 100, (-rb.velocity.y * playerpos.knockbackForce) / 100);
        //Transform originalFirePoint = this.transform;
        //rb.AddForce(originalFirePoint.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
    }

    public void SetDamageBUllet(float _multiplier)
    {
        bulletData.bulletDamage = 34 * _multiplier;
    }
    
    protected override void Update()
    {
        base.Update();

        if (!powerUpOn)
        {
           
        }
        else
        {
            
        }

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit"))
        {
            base.ImpactWall();
            Debug.Log("Hit the wall");
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            bulletInfo.damage = bulletData.bulletDamage;
            bulletInfo.impactPosition = transform.position;
            collision.gameObject.SendMessage("GetDamage", bulletInfo);
            base.ImpactBody();           
        }
    }


    
}
