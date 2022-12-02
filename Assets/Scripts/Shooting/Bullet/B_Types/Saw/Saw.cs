using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Saw : Bullet
{
    private Rigidbody2D rb;
    private ElectricGun thisGun;
    Transform originalFirePoint;
    public bool isAlive;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //thisGun.bulletsOut.Add(this.gameObject);

        bulletDamage = 20 * _damageMultiplier;
        bulletRangeInMetres = 100;
        bulletSpeedMetresPerSec = 30;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        originalFirePoint = this.transform;
        originalFirePoint.Rotate(0, 0, originalFirePoint.transform.rotation.z + Random.Range(-15, 15));
        rb.AddForce(originalFirePoint.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    protected override void Update()
    {


        base.Update();
        bulletSpeedMetresPerSec -= 3f * Time.deltaTime;

    }
}




