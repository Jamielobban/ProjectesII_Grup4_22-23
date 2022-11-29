using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalasExplosivas : Bullet
{
    private Rigidbody2D rb;


    public GameObject explos;
    protected override void Start()
    {
        base.Start();

        bulletDamage = 5 * _damageMultiplier;
        bulletRangeInMetres = 100;
        bulletSpeedMetresPerSec = 50;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();


        transform.Rotate(0, 0, transform.rotation.z + Random.Range(-10, 10));

        //Transform originalFirePoint = this.transform;
        rb.AddForce(-this.transform.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        StartCoroutine(explosions(0.1f));
    }

    private IEnumerator explosions(float time)
    {
        yield return new WaitForSeconds(time);


        StartCoroutine(explosions(0.1f));

        GameObject explosion = Instantiate(explos, transform.position, transform.rotation);

        explosion.GetComponent<Bullet>().ApplyMultiplierToDamage(_damageMultiplier);
    }


    protected override void Update()
    {
        base.Update();
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit"))
        {
            base.ImpactWall();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            bulletInfo.damage = bulletDamage;
            bulletInfo.impactPosition = transform.position;
            collision.gameObject.SendMessage("GetDamage", bulletInfo);
            base.HitSomeone();

        }
    }



}
