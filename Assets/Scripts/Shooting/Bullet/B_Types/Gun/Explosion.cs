using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Bullet
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        bulletData.bulletDamage = damage * bulletData._damageMultiplier;

        Destroy(this.gameObject, 0.6f);

    }

    protected override void Update()
    {
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Enemy"))
         {
            collision.gameObject.GetComponent<Entity>().GetDamage(bulletData.bulletDamage, HealthStateTypes.NORMAL, 0, this.transform.position, TransformMovementType.SHAKE);
            base.HitSomeone();
         }
    }
}
