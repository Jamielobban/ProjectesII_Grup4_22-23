using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalasExplosivas : Bullet
{
    
    public GameObject explos;
    private PlayerMovement playerpos;
    private RecoilScript _recoil;
    private Turn turningPoint;
    public GameObject bulletShell;
    public Turn will;
    private GameObject rotatePoint;
    public GameObject muzzleFlash;
    protected override void Start()
    {
        base.Start();

        rotatePoint = GameObject.FindGameObjectWithTag("FirePoint");
        playerpos = FindObjectOfType<PlayerMovement>();
        _recoil = FindObjectOfType<RecoilScript>();
        will = FindObjectOfType<Turn>();

        //bulletShell = Resources.Load<GameObject>("Prefab/BulletParticle");
        Instantiate(muzzleFlash, rotatePoint.transform.position, rotatePoint.transform.rotation);
        Instantiate(bulletShell,will.transform.position,Quaternion.identity);
        //bulletDamage = 5 * _damageMultiplier;
        //bulletRangeInMetres = 100;
        //bulletSpeedMetresPerSec = 50;
        //bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        bulletData.bulletDamage *= bulletData._damageMultiplier;
        CinemachineShake.Instance.ShakeCamera(5f, .2f);


        _recoil.AddRecoil();

        playerpos.knockback = true;
        playerpos.rb.velocity = new Vector2((-rb.velocity.x * 0.5f), (-rb.velocity.y * 0.5f));
        //transform.Rotate(0, 0, transform.rotation.z + Random.Range(-10, 10));
        //Transform originalFirePoint = this.transform;
        //rb.AddForce(-this.transform.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        StartCoroutine(explosions(Random.Range(0.15f, 0.25f)));
    }

    private IEnumerator explosions(float time)
    {
        yield return new WaitForSeconds(time);
        Instantiate(explos, transform.position, transform.rotation);
        CinemachineShake.Instance.ShakeCamera(3f, .2f);
        Destroy(gameObject);

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
