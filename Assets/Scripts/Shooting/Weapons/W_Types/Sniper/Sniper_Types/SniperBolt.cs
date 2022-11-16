using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBolt : Sniper
{
    public SniperBolt(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
    {        
        weaponMechanism = new Repeticion();

    }

    protected override void ActionOnEnterPowerup()
    {
        ShootPowerUp(data.bulletTypePrefab, firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue);

        //data.timelastPowerupEnter.RuntimeValue = 0;
    }

    private void ShootPowerUp(GameObject bulletTypePrefab, Transform firePoint, float fireRateinSec, AudioClip shootSound, float amplitudeGain, float damageMultiplier)
    {
        GameObject bullet = GameObject.Instantiate(bulletTypePrefab, firePoint.position, firePoint.rotation);

        bullet.GetComponent<Bullet>().powerUpOn = true; ;

        bullet.GetComponent<Bullet>().ApplyMultiplierToDamage(damageMultiplier);
        AudioManager.Instance.PlaySound(shootSound);
        CinemachineShake.Instance.ShakeCamera(5f, .1f);
        CinemachineShake.Instance.ShakeCamera(5f * amplitudeGain, .1f);
    }
}

//, new Repeticion()
//WeaponGenerator.Instance.SetMechanismToWeapon(ref mechanism, 0);
//data.fireRateinSec *= 0.35f;
//data.fireRateinSec /= 60f; //Aqui es dps
//data.fireRateinSec = 1 / data.fireRateinSec; //Aqui calculem el minim temps possible entre disparos
//data.shootSound = Resources.Load<AudioClip>("Sounds/Weapons/Sniper/sniperCerrojo_effect");
//data.weaponColor = Color.blue;
//data.damageMultiplier = 1.5f;
//data.amplitudeGain = 2f;
//data.bulletsPerMagazine -= 2;
//ata.currentBulletsInMagazine = data.BulletsPerMagazine;