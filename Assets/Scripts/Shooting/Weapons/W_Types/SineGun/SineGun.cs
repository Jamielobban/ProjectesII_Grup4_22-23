using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineGun : Weapon
{
    int? powerupEmptyKey;
    int? shootSoundKey;
    public SineGun(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
    {
        weaponMechanism = new Repeticion();
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
            powerupEmptyKey = AudioManager.Instance.LoadSound(powerupEmpty, player.transform.transform);
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
        shootSoundKey = AudioManager.Instance.LoadSound(data.shootSound, bullet.transform.position);
        CinemachineShake.Instance.ShakeCamera(5f, .1f);
        CinemachineShake.Instance.ShakeCamera(5f * data.amplitudeGain.RuntimeValue, .1f);
    }

    protected override bool CheckPowerUpShooting()
    {
        return false;
    }
}
