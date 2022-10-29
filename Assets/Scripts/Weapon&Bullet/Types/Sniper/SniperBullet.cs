using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    protected override void Start()
    {
        base.Start();

        bulletDamage = 50;
        bulletRangeInMetres = 10000;
        bulletSpeedMetresPerSec = 30;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();
        bc = this.GetComponent<BoxCollider2D>();

        Transform originalFirePoint = this.transform;
        rb.AddForce(originalFirePoint.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
    }

    protected override void Update()
    {
        base.Update();

        Physics2D.Raycast(bc.transform.position, rb.velocity.normalized, 2, whatIsMapColisionable);
        Debug.DrawRay(bc.transform.position, rb.velocity.normalized*0.3f, Color.white);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit") && !powerUpOn)
        {            
            base.Impact();
            
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            HitSomeone();
        }
    }        

    private void HitSomeone()
    {
        this.GetComponent<CircleCollider2D>().isTrigger = true;
        this.GetComponent<Collider2D>().isTrigger = true;
        Instantiate(collisionEffect, transform.position, Quaternion.identity);
    }

    private void PowerUpImpact(Vector2 pointOfContact)
    {

        Instantiate(collisionEffect, transform.position, Quaternion.identity);
        this.GetComponent<CircleCollider2D>().isTrigger = true;
        //Vector2 myRigidbodyVelocity = this.GetComponent<Rigidbody2D>().velocity;


        if (Mathf.Abs(transform.position.x - pointOfContact.x) > Mathf.Abs(transform.position.y - pointOfContact.y))
        {
            this.GetComponent<Rigidbody2D>().velocity *= new Vector2(-1, 1);
        }
        else if (Mathf.Abs(transform.position.x - pointOfContact.x) < Mathf.Abs(transform.position.y - pointOfContact.y))
        {
            this.GetComponent<Rigidbody2D>().velocity *= new Vector2(1, -1);
        }
        else
        {
            this.GetComponent<Rigidbody2D>().velocity *= new Vector2(-1, -1);
        }

        //if (myRigidbodyVelocity.x > 0 && myRigidbodyVelocity.y > 0)
        //{
        //    if(Mathf.Abs(transform.position.x-pointOfContact.x)> Mathf.Abs(transform.position.y - pointOfContact.y))
        //    {
        //        this.GetComponent<Rigidbody2D>().velocity *= new Vector2(-1, 1);
        //    }
        //    else if(Mathf.Abs(transform.position.x - pointOfContact.x) < Mathf.Abs(transform.position.y - pointOfContact.y))
        //    {
        //        this.GetComponent<Rigidbody2D>().velocity *= new Vector2(1, -1);

        //    }
        //    else
        //    {
        //        this.GetComponent<Rigidbody2D>().velocity *= new Vector2(-1, -1);
        //    }
        //}
        //else if (myRigidbodyVelocity.x < 0 && myRigidbodyVelocity.y < 0)
        //{
        //    if (Mathf.Abs(transform.position.x - pointOfContact.x) > Mathf.Abs(transform.position.y - pointOfContact.y))
        //    {
        //        this.GetComponent<Rigidbody2D>().velocity *= new Vector2(-1, 1);
        //    }
        //    else if (Mathf.Abs(transform.position.x - pointOfContact.x) < Mathf.Abs(transform.position.y - pointOfContact.y))
        //    {
        //        this.GetComponent<Rigidbody2D>().velocity *= new Vector2(-1, -1);

        //    }
        //}
    }
}
