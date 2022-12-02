using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawgun : Weapon
{
    public Sawgun(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
    {
        weaponMechanism = new Automatica();
    }

    public override void Update()
    {
        base.Update();

        if (Time.time - data.timelastPowerupEnter.RuntimeValue >= data.maxTimeOnPowerup.RuntimeValue && data.powerActive.RuntimeValue)
        {
            data.powerActive.RuntimeValue = false;
            data.powerupAvailable.RuntimeValue = false;
            //data.fireRateinSec /= 0.5f;
            //data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
            data.timelastPowerupExit.RuntimeValue = Time.time;
            AudioManager.Instance.PlaySound(powerupEmpty, player.transform);
        }
        if (!data.powerActive.RuntimeValue)
        {
            data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
        }
        else
        {
            data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = true;
        }
    }

    protected override void ActionOnEnterPowerup()
    {
        base.ActionOnEnterPowerup();

        data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = true; 

        GameObject bullet = GameObject.Instantiate(data.bulletTypePrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().ApplyMultiplierToDamage(data.damageMultiplier.RuntimeValue);
        AudioManager.Instance.PlaySound(data.shootSound, bullet.transform.position);
        CinemachineShake.Instance.ShakeCamera(5f, .1f);
        CinemachineShake.Instance.ShakeCamera(5f * data.amplitudeGain.RuntimeValue, .1f);
    }

    protected override void CheckPowerUpShooting()
    {
        
    }
}
