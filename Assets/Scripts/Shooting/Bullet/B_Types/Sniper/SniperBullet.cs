using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : Bullet
{
    
    [SerializeField]
    private GameObject childForCollisions;

    public LayerMask enemy;
    protected override void Start()
    {
        base.Start();

        //bulletDamage = 150*_damageMultiplier;
        //bulletRangeInMetres = 150;
        //bulletSpeedMetresPerSec = 100;
        //bulletRadius = 0.23f;

       

        bulletData.bulletDamage *= bulletData._damageMultiplier;

       

        CinemachineShake.Instance.ShakeCamera(40f, .2f);

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

            base.ImpactWall();

        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            base.HitSomeone();
            //bulletInfo.damage = bulletDamage;
            //bulletInfo.impactPosition = transform.position;
            collision.GetComponent<EnemyController>().GetDamage(() =>
            {
                AudioManager.Instance.PlaySound(collision.GetComponent<EnemyController>().damageSound, this.transform.position);
                GameObject blood = GameObject.Instantiate(collision.GetComponent<EnemyController>().floorBlood, this.transform.position, this.transform.rotation);
                blood.GetComponent<Transform>().localScale = transform.localScale * 2;

                if (collision.GetComponent<EnemyController>().enemyHealth <= bulletData.bulletDamage)
                {
                    collision.GetComponent<EnemyController>().enemyHealth = 0;
                    collision.GetComponent<EnemyController>().isDeath = true;
                }
                else
                {
                    collision.GetComponent<EnemyController>().enemyHealth -= bulletData.bulletDamage;
                }
            });
        }
    }  
    

    

   
}
