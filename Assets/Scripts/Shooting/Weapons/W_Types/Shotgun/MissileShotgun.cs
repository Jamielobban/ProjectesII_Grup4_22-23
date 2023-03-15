//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class MissileShotgun : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileShotgun : Bullet
{

    private PlayerMovement playerpos;
    private RecoilScript _recoil;
    private Turn turningPoint;
    public GameObject bulletShell;
    public Turn will;
    private GameObject rotatePoint;
    public GameObject muzzleFlash;
    private Rigidbody2D rbMissile;
    protected override void Start()
    {
        base.Start();

        rbMissile = GetComponent<Rigidbody2D>();

        rotatePoint = GameObject.FindGameObjectWithTag("PlayerFirePoint");
        playerpos = FindObjectOfType<PlayerMovement>();
        _recoil = FindObjectOfType<RecoilScript>();
        will = FindObjectOfType<Turn>();


        Instantiate(muzzleFlash, rotatePoint.transform.position, rotatePoint.transform.rotation);
        Instantiate(bulletShell, will.transform.position, Quaternion.identity);

        bulletData.bulletDamage *= bulletData._damageMultiplier;
        CinemachineShake.Instance.ShakeCamera(5f, .2f);


        _recoil.AddRecoil();

        playerpos.knockback = true;
        playerpos.rb.velocity = new Vector2((-rb.velocity.x * 2f), (-rb.velocity.y * 2f));
    }



    protected override void Update()
    {
        base.Update();
        bulletData.bulletSpeedMetresPerSec.RuntimeValue += 0.5f;
        rbMissile.velocity = (bulletData.bulletSpeedMetresPerSec.RuntimeValue) * rb.velocity.normalized;
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

    private void OnDestroy()
    {
        bulletData.bulletSpeedMetresPerSec.RuntimeValue = bulletData.bulletSpeedMetresPerSec.InitialValue;
    }


}
