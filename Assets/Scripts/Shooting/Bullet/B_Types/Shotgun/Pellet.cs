using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Pellet : Bullet
{       
    [SerializeField]
    SpriteRenderer sr;

    private PlayerMovement playerpos;
    private RecoilScript _recoil;

    protected override void Start()
    {
        _recoil = FindObjectOfType<RecoilScript>();
        base.Start();
        playerpos = FindObjectOfType<PlayerMovement>();
        //bulletDamage = 34*_damageMultiplier;
        //bulletRangeInMetres = 500;
        //bulletSpeedMetresPerSec = 20;
        //bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();
        _recoil.AddRecoil();

        bulletData.bulletDamage *= bulletData._damageMultiplier;
        playerpos.knockback = true;
        playerpos.rb.velocity = new Vector2((-rb.velocity.x * 0.5f), (-rb.velocity.y * 0.5f));
        CinemachineShake.Instance.ShakeCamera(10f, 0.3f);
        
        //Transform originalFirePoint = this.transform;
        //rb.AddForce(originalFirePoint.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
    }

    public void SetDamageBUllet(float _multiplier)
    {
        bulletData.bulletDamage = 34 * _multiplier;
    }
    
    protected override void Update()
    {
        base.Update();

        if (!powerUpOn)
        {
           
        }
        else
        {
            
        }

        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit"))
        {
            base.ImpactWall();
            //Debug.Log("Hit the wall");
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Entity>().GetDamage(bulletData.bulletDamage, HealthStateTypes.NORMAL, 0, this.transform.position, TransformMovementType.PUNCH);
            base.ImpactBody();           
        }
    }


    
}
