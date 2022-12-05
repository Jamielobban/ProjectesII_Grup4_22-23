using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBullet : Bullet
{
    
    public ElectricGun thisGun;
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        bulletData.bulletDamage *= bulletData._damageMultiplier;
        //bulletDamage = 20 * _damageMultiplier;
        //bulletRangeInMetres = 100;
        //bulletSpeedMetresPerSec = 20;
        //bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        originalFirePoint = this.transform;
        originalFirePoint.Rotate(0, 0, originalFirePoint.transform.rotation.z + Random.Range(-5, 5));
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
    }
}




