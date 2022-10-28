using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{

    [SerializeField]
    protected GameObject collisionEffect;

    [SerializeField]
    private LayerMask whatIsDamageable;
    [SerializeField]
    private LayerMask whatIsMapColisionable;

    protected float bulletDamage;
    protected float bulletSpeedMetresPerSec;
    protected float bulletRangeInMetres;
    protected float bulletRadius;
    protected bool enemyHit;

    private bool hitSomething;
    private bool outOfRange;
    private float timeShooted;


    protected virtual void Start()
    {
        //Fer particulas

        hitSomething = false;
        outOfRange = false;
        timeShooted = Time.time;
        enemyHit = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (Time.time - timeShooted >= bulletRangeInMetres / bulletSpeedMetresPerSec)
        {
            Destroy(this.gameObject);

        }
        //else if (mapLimitHit)
        //{
        //    Impact();

        //}
        //else if (damageHit)
        //{
        //    //Fer damage
        //    enemyHit = true;
        //}

    }

    protected virtual void Impact()
    {
        Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}