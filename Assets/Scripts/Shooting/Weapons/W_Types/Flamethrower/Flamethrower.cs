using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : Weapon
{
    public Flamethrower(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
    {
        weaponMechanism = new Flow();

    }
    protected override void ActionOnEnterPowerup()
    {
        base.ActionOnEnterPowerup();
        
    }

    protected override void CheckPowerUpShooting()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    public override void Update()
    {
        base.Update();

        if (!data.powerActive.RuntimeValue)
        {
            data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = false;
        }

        if (Time.time - data.timelastPowerupEnter.RuntimeValue >= data.maxTimeOnPowerup.RuntimeValue && data.powerActive.RuntimeValue)
        {
            data.powerActive.RuntimeValue = false;
            data.powerupAvailable.RuntimeValue = false;
            data.timelastPowerupExit.RuntimeValue = Time.time;
            AudioManager.Instance.PlaySound(powerupEmpty);
        }
    }
}
