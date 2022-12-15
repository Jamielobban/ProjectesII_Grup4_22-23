using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalasExplosivas : Bullet
{
    
    public GameObject explos;
    private PlayerMovement playerpos;
    protected override void Start()
    {
        base.Start();
        playerpos = FindObjectOfType<PlayerMovement>();
        //bulletDamage = 5 * _damageMultiplier;
        //bulletRangeInMetres = 100;
        //bulletSpeedMetresPerSec = 50;
        //bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        bulletData.bulletDamage *= bulletData._damageMultiplier;
        CinemachineShake.Instance.ShakeCamera(5f, .2f);


        playerpos.knockback = true;
        playerpos.rb.velocity = new Vector2((-rb.velocity.x * playerpos.knockbackForce) / 100, (-rb.velocity.y * playerpos.knockbackForce) / 100);
        //transform.Rotate(0, 0, transform.rotation.z + Random.Range(-10, 10));
        Debug.Log("MAKING A BULLET");
        //Transform originalFirePoint = this.transform;
        //rb.AddForce(-this.transform.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        StartCoroutine(explosions(0.1f));
    }

    private IEnumerator explosions(float time)
    {
        yield return new WaitForSeconds(time);


        StartCoroutine(explosions(0.3f));

        GameObject explosion = Instantiate(explos, transform.position, transform.rotation);
        CinemachineShake.Instance.ShakeCamera(3f, .2f);

        //explosion.GetComponent<Bullet>().ApplyMultiplierToDamage(bulletData._damageMultiplier);
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
            bulletInfo.damage = bulletData.bulletDamage;
            bulletInfo.impactPosition = transform.position;
            //collision.gameObject.SendMessage("GetDamage", bulletInfo);
            collision.gameObject.GetComponent<Entity>().GetDamage(bulletData.bulletDamage, HealthStateTypes.NORMAL, 0, this.transform.position, TransformMovementType.PUNCH);
            base.HitSomeone();

        }
    }



}
