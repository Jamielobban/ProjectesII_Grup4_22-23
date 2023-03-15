using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SniperBullet : Bullet
{
    
    [SerializeField]
    private GameObject childForCollisions;

    private RecoilScript _recoilScript;
    public LayerMask enemy;
    public ParticleSystem effect;
    private PlayerMovement playerpos;
    private float angle;
    private float knockbackForce = 10;
    private Canvas myCavnas;
    protected override void Start()
    {
        base.Start();
        myCavnas= FindObjectOfType<Canvas>();
        _recoilScript = FindObjectOfType<RecoilScript>();
        playerpos = FindObjectOfType<PlayerMovement>();
        //bulletDamage = 150*_damageMultiplier;
        //bulletRangeInMetres = 150;
        //bulletSpeedMetresPerSec = 100;
        //bulletRadius = 0.23f;

        //myCavnas.DO

        _recoilScript.AddRecoil();
        angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg - 90;
        bulletData.bulletDamage *= bulletData._damageMultiplier;

        playerpos.knockback = true;
        playerpos.rb.velocity = new Vector2((-rb.velocity.x * 0.75f), (-rb.velocity.y * 0.75f));
        //playerpos.rb.velocity =  new Vector2((-rb.velocity.x * playerpos.knockbackForce) /100, (-rb.velocity.y*playerpos.knockbackForce) /100);

        Instantiate(effect, playerpos.transform.position, Quaternion.identity);
        CinemachineShake.Instance.ShakeCamera(10f, 0.75f);
    }

    protected override void Update()
    {
        base.Update();

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
            collision.gameObject.GetComponent<Entity>().GetDamage(bulletData.bulletDamage, HealthStateTypes.NORMAL, 0, this.transform.position, TransformMovementType.PUNCH);

        }
    }  
    

    

   
}
