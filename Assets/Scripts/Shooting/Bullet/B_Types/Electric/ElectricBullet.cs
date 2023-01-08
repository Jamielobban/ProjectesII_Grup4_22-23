using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBullet : Bullet
{
    
    public ElectricGun thisGun;
    private PlayerMovement playerpos;
    private RecoilScript recoilScript;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerpos = FindObjectOfType<PlayerMovement>();
        recoilScript = FindObjectOfType<RecoilScript>();
        bulletData.bulletDamage *= bulletData._damageMultiplier;
        //bulletDamage = 20 * _damageMultiplier;
        //bulletRangeInMetres = 100;
        //bulletSpeedMetresPerSec = 20;
        //bulletRadius = 0.23f;

        playerpos.knockback = true;
        playerpos.rb.velocity = new Vector2((-rb.velocity.x * playerpos.knockbackForce) / 100, (-rb.velocity.y * playerpos.knockbackForce) / 100);

        recoilScript.AddRecoil();
        //originalFirePoint.Rotate(0, 0, originalFirePoint.transform.rotation.z + Random.Range(-5, 5));
        //rb.AddForce(originalFirePoint.up  * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MapLimit"))
        {
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Entity>().GetDamage(bulletData.bulletDamage, HealthStateTypes.NORMAL, 0, this.transform.position, TransformMovementType.PUNCH);
            ImpactBody();
        }
        
    }
}




