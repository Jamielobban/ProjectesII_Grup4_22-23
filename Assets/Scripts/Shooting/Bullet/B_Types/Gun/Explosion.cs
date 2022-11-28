using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        bulletDamage = 50 * _damageMultiplier;
        Destroy(this.gameObject, 0.6f);

    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Enemy"))
        {
            base.HitSomeone();
            bulletInfo.damage = bulletDamage;
            bulletInfo.impactPosition = transform.position;
            collision.gameObject.SendMessage("GetDamage", bulletInfo);
        }
    }
}
