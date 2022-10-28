using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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

    //[SerializeField]
    //private GameObject[] damageableEntities;
    //[SerializeField]
    //private GameObject[] noDamageableEntities;

    // Start is called before the first frame update
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

        Collider2D mapLimitHit = Physics2D.OverlapCircle(this.transform.position, bulletRadius, whatIsMapColisionable);
        Collider2D damageHit = Physics2D.OverlapCircle(this.transform.position, bulletRadius, whatIsDamageable);

        if (Time.time - timeShooted >= bulletRangeInMetres / bulletSpeedMetresPerSec)
        {
            Destroy(this.gameObject);
            
        }
        else if (mapLimitHit)
        {
            Impact();
            
        }
        else if (damageHit)
        {
            //Fer damage
            enemyHit = true;
        }

    }

    protected virtual void Impact()
    {
        Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    protected void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, bulletRadius);

    }
}
