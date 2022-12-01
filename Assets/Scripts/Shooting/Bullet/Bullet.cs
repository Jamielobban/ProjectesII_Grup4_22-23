using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public bool powerUpOn;
    protected BulletHitInfo bulletInfo;

    [SerializeField]
    protected GameObject collisionWallEffect;
    [SerializeField]
    protected GameObject collisionBloodEffect;

    [SerializeField]
    private LayerMask whatIsDamageable;
    [SerializeField]
    protected LayerMask whatIsMapColisionable;

    protected float bulletDamage;
    protected float bulletSpeedMetresPerSec;
    protected float bulletRangeInMetres;
    protected float bulletRadius;
    protected float _damageMultiplier;

    private bool firstTime = true;
    protected float timeShooted;
    
    public virtual void ApplyMultiplierToDamage(float multiplier)
    {
        _damageMultiplier = multiplier;        
    }

    public float GetDamageMultiplier()
    {
        return _damageMultiplier;
    }
    public float GetBUlletDamage()
    {
        return bulletDamage;
    }

    public float GetSpeed()
    {
        return bulletSpeedMetresPerSec;
    }

    protected virtual void Start()
    {
        //Fer particulas       
        
        timeShooted = Time.time;       
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (firstTime)
        {

            firstTime = false;
        }

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

    protected virtual void ImpactWall()
    {
        Instantiate(collisionWallEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    protected virtual void ImpactBody()
    {
        Instantiate(collisionBloodEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    protected virtual void HitSomeone()
    {
        Instantiate(collisionBloodEffect, transform.position, Quaternion.identity);
    }
    protected virtual void HitSomething()
    {
        Instantiate(collisionWallEffect, transform.position, Quaternion.identity);
    }
}