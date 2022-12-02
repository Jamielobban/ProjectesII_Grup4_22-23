using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Saw : Bullet
{
    private Rigidbody2D rb;
    private ElectricGun thisGun;
    Transform originalFirePoint;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //thisGun.bulletsOut.Add(this.gameObject);

        bulletDamage = 20 * _damageMultiplier;
        bulletRangeInMetres = 100;
        bulletSpeedMetresPerSec = 20;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        originalFirePoint = this.transform;
        originalFirePoint.Rotate(0, 0, originalFirePoint.transform.rotation.z + Random.Range(-15, 15));
        rb.AddForce(originalFirePoint.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    protected override void Update()
    {
        //transform.DORotate(new Vector3(1f, 1f, 1f * Time.deltaTime), 5f, RotateMode.LocalAxisAdd);
        base.Update();
    }
}




