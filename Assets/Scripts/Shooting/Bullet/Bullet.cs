using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public bool powerUpOn;
    protected BulletHitInfo bulletInfo;

    //[SerializeField]
    public GameObject collisionWallEffect;
    [SerializeField]
    protected GameObject collisionBloodEffect;

    [SerializeField]
    private LayerMask whatIsDamageable;
    [SerializeField]
    protected LayerMask whatIsMapColisionable;

    protected Transform originalFirePoint;

    [SerializeField]
    protected Rigidbody2D rb;

    
    public D_Bullet bulletData;

   

    private bool firstTime = true;
    protected float timeShooted;
    
    public virtual void ApplyMultiplierToDamage(float multiplier)
    {
        bulletData._damageMultiplier = multiplier;        
    }

    public float GetDamageMultiplier()
    {
        return bulletData._damageMultiplier;
    }
    public float GetBUlletDamage()
    {
        return bulletData.bulletDamage;
    }

    public float GetSpeed()
    {
        return bulletData.bulletSpeedMetresPerSec;
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

        if (Time.time - timeShooted >= bulletData.bulletRangeInMetres / bulletData.bulletSpeedMetresPerSec)
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

    public void FireProjectile(/*Transform referenceTransform*/)
    {
        //this.speed = speed;
        //this.travelDistance = travelDistance;

        //attackDetails.damageAmount = damage;        

        //originalFirePoint = referenceTransform;
        originalFirePoint = this.transform;
        originalFirePoint.Rotate(0f, 0f, originalFirePoint.transform.rotation.z + Random.Range(bulletData.minRangeTransform, bulletData.maxRangeTransform));
        
        //Debug.Log(originalFirePoint == null);
        Debug.Log(bulletData);
        if (bulletData.ApplyShootForce)
        {
            Debug.Log(originalFirePoint);
            Debug.Log("in");
            this.GetComponent<Rigidbody2D>().AddForce(originalFirePoint.up * -bulletData.bulletSpeedMetresPerSec, ForceMode2D.Impulse);
            
        }

    }
}