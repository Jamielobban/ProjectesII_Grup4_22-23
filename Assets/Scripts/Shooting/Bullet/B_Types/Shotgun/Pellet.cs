using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pellet : Bullet
{
    private Rigidbody2D rb;
    [SerializeField]
    private GameObject childExplosionArea;
    [SerializeField]
    SpriteRenderer sr;
    
    protected override void Start()
    {
        base.Start();

        bulletDamage = 34*_damageMultiplier;
        bulletRangeInMetres = 5;
        bulletSpeedMetresPerSec = 20;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        
        //Transform originalFirePoint = this.transform;
        //rb.AddForce(originalFirePoint.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
    }

    public void SetDamageBUllet(float _multiplier)
    {
        bulletDamage = 34 * _multiplier;
    }
    
    protected override void Update()
    {
        base.Update();

        if (!powerUpOn)
        {
            childExplosionArea.SetActive(false);
        }
        else
        {
            bulletRangeInMetres =15;
        }

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit"))
        {
            base.ImpactWall();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            if(!powerUpOn)
                base.ImpactBody();
            else
            {
                //Debug.Log("Hi");
                base.HitSomeone();
                sr.enabled = false;
                this.GetComponent<Pellet>().enabled = false;
                childExplosionArea.SetActive(true);
                this.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                collision.gameObject.SendMessage("GetDamage", bulletDamage);
            }
        }
    }


    
}
