using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Misil : Bullet
{
    
    GameObject[] enemies;
    public GameObject enemy;
    float minRotation;
    public float rotateSpeed;
 
    // Start is called before the first frame update
    void Start()
    {
        //bulletDamage = 10 * _damageMultiplier;
        //bulletRangeInMetres = 1000000000;
        //bulletSpeedMetresPerSec = 50;
        //bulletRadius = 0.23f;

        enemy = null;

        minRotation = 0;
        bulletData.bulletDamage *= bulletData._damageMultiplier;

        rb = this.GetComponent<Rigidbody2D>();
        this.transform.Rotate(0, 0, this.transform.rotation.z + Random.RandomRange(-20, 20));

        //enemies = GameObject.FindGameObjectsWithTag("Enemy");

        //for (int i = 0; i < enemies.Length; i++)
        //{
        //    Vector2 direction = (Vector2)enemies[i].transform.position - rb.position;

        //    direction.Normalize();


        //    float rotateAmount = Vector3.Cross(direction, transform.up).z;

        //    if (rotateAmount < minRotation)
        //    {
        //        minRotation = rotateAmount;
        //        enemy = enemies[i];

        //    }

        //}

  
         //this.GetComponent<Rigidbody2D>().AddForce(-this.transform.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //if (enemy != null)
        //{


        //    Vector2 direction = (Vector2)enemy.transform.position - (Vector2)this.transform.position;

        //    direction.Normalize();


        //    float rotateAmount = Vector3.Cross(direction, transform.up).z;

      
        //    rb.angularVelocity = rotateAmount*rotateSpeed;


        //    rb.velocity = -transform.up * bulletSpeedMetresPerSec;
        //}

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
            bulletInfo.damage = bulletData.bulletDamage;
            bulletInfo.impactPosition = transform.position;
            collision.gameObject.SendMessage("GetDamage", bulletInfo);
        }
    }
}
