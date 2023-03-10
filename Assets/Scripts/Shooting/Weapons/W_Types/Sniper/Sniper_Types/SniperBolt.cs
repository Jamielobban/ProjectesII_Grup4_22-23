//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class SniperBolt : Sniper
//{
//    int? shootSoundKey;
//    public SniperBolt(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
//    {        
//        weaponMechanism = new Repeticion();

//    }

//    public override void Update()
//    {
//        base.Update();
//        //Debug.Log(GetIfOutOffAmmo());
//    }

//    protected override void ActionOnEnterPowerup()
//    {
//        ShootPowerUp(data.bulletTypePrefab, firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue);

//        //data.timelastPowerupEnter.RuntimeValue = 0;
//    }

//    private void ShootPowerUp(GameObject bulletTypePrefab, Transform firePoint, float fireRateinSec, AudioClip shootSound, float amplitudeGain, float damageMultiplier)
//    {
//        GameObject bullet = GameObject.Instantiate(bulletTypePrefab, firePoint.position, firePoint.rotation);

//        bullet.GetComponent<Bullet>().powerUpOn = true; ;

//        bullet.GetComponent<Bullet>().ApplyMultiplierToDamage(damageMultiplier);
//        shootSoundKey = AudioManager.Instance.LoadSound(shootSound,bullet.transform.position);
//        CinemachineShake.Instance.ShakeCamera(5f, .1f);
//        CinemachineShake.Instance.ShakeCamera(5f * amplitudeGain, .1f);
//    }
//}
