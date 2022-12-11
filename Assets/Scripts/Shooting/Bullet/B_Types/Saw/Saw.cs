using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Saw : Bullet
{
    //private Rigidbody2D rb;
    
    //Transform originalFirePoint;
    public bool isAlive;
    private RecoilScript _recoilScript;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    protected override void Start()
    {
        _recoilScript = FindObjectOfType<RecoilScript>();
        base.Start();

        //thisGun.bulletsOut.Add(this.gameObject);

        //bulletDamage = 20 * _damageMultiplier;
        //bulletRangeInMetres = 100;
        //bulletSpeedMetresPerSec = 30;
        //bulletRadius = 0.23f;

        //rb = this.GetComponent<Rigidbody2D>();

        bulletData.bulletDamage *= bulletData._damageMultiplier;
        _recoilScript.AddRecoil();

        //originalFirePoint.Rotate(0, 0, originalFirePoint.transform.rotation.z + Random.Range(-15, 15));

        //rb.AddForce(originalFirePoint.up * -bulletData.bulletSpeedMetresPerSec, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    protected override void Update()
    {


        base.Update();
        //bulletData.bulletSpeedMetresPerSec -= 3f * Time.deltaTime;

    }
}




