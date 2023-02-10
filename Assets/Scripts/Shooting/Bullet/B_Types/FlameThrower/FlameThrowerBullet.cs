using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.ParticleSystemJobs;

public class FlameThrowerBullet : Bullet
{
    public bool isClicking;
    private BlitController myblit;

    protected override void Start()
    {
        timeShooted = Time.time;
        //playerpos = FindObjectOfType<PlayerMovement>();
        myblit = FindObjectOfType<BlitController>();
        bulletData.bulletDamage *= bulletData._damageMultiplier;
        bulletData.bulletRangeInMetres = FindObjectOfType<RightHand>().GetWeaponInHand().GetFireRate();
        //myblit._Fired = true;
        //bulletDamage = 0.5f * _damageMultiplier;
        //bulletRangeInMetres = FindObjectOfType<RightHand>().GetWeaponInHand().GetFireRate();
        //bulletSpeedMetresPerSec = 1;
        //bulletRadius = 0.23f;
        //Debug.Log("creatng bullet");
        isClicking = true;
//rb = this.GetComponent<Rigidbody2D>();

    }

    protected override void Update()
    {
        //if (myblit._Fired)
        //{
        //    isClicking = true;
        //    myblit._Percentage += Time.deltaTime * 2;
        //    myblit._Mat.SetFloat("_Percent", myblit._Percentage);
        //    if (myblit._Percentage > 1)
        //    {
        //        myblit._Percentage = 0;
        //        myblit._Fired = false;
        //    }
        //    myblit._Fired = true;
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    myblit._Fired = false;
        //}
        //CinemachineShake.Instance.ShakeCamera(4f, .05f);
        //base.Update();
        //timeShooted = Time.time;        

    }
    public float GetFlamesDamage()
    {
        return bulletData.bulletDamage;
    }

}
