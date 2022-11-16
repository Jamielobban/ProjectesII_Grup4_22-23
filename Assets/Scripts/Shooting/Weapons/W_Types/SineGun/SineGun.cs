using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineGun : Weapon
{
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
            AudioManager.Instance.PlaySound(powerupEmpty);
        }
        if (!data.powerActive)
        {
            data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
        }
    }

    protected override void ActionOnEnterPowerup()
    {
        base.ActionOnEnterPowerup();
    }

    protected override void CheckPowerUpShooting()
    {
        throw new System.NotImplementedException();
    }
}
