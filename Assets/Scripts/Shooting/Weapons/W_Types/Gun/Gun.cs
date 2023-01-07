using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : Weapon
{
    protected GameObject secondHandPrefab;
    protected GameObject secondHandClone;
    protected Mechanism temporalMechanism;
    int? powerupEmptyKey;
    public Gun(Transform _firePoint, WeaponValues _data) : base(_firePoint, _data)
    {
        //data.bulletsPerMagazine = Random.Range(13, 20);
        //data.magazines = Random.Range(1, 2); // 4/7
        //data.reloadTimeInSec = 2f;
        //data.maxTimeOnPowerup = 8f;
        //data.currentBulletsInMagazine = data.BulletsPerMagazine;
        //data.currentMagazines = data.Magazines;
        //data.fireRateinSec = Random.Range(660f, 700f); //Aqui esta en dpm
        //data.weaponSprite = Resources.Load<Sprite>("Sprites/Pistol"); 
        //data.reloadSound = Resources.Load<AudioClip>("Sounds/Weapons/Pistol/gunreload");
        //data.bulletTypePrefab = Resources.Load<GameObject>("Prefab/GunBullet");
        //data.amplitudeGain = 0f;
        secondHandPrefab = Resources.Load<GameObject>("Prefab/LeftHand");
    }

    
    protected override bool CheckPowerUpShooting()
    {        
        //No gastar municio
        return weaponMechanism.Shoot(data.bulletTypePrefab, firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue);    
    }

    public override void Update()
    {
        base.Update();

        if (Time.time - data.timelastPowerupEnter.RuntimeValue >= data.maxTimeOnPowerup.RuntimeValue && data.powerActive.RuntimeValue)
        {
            data.powerActive.RuntimeValue = false;
            GameObject.Destroy(secondHandClone.gameObject);
            
            data.powerupAvailable.RuntimeValue = false;                    
            data.timelastPowerupExit.RuntimeValue = Time.time;
            powerupEmptyKey = AudioManager.Instance.LoadSound(powerupEmpty, player.transform);
        }
    }

    protected override void ActionOnEnterPowerup()
    {
        //Fer duales 
        secondHandClone = GameObject.Instantiate(secondHandPrefab);
        secondHandClone.GetComponent<SpriteRenderer>().sprite = data.weaponSprite;
        secondHandClone.GetComponent<SpriteRenderer>().color = data.weaponColor;
        secondHandClone.GetComponent<LeftHand>().lifeTime = data.maxTimeOnPowerup.RuntimeValue;
        secondHandClone.GetComponent<LeftHand>().weaponInHand = this;
        //Recarga automatica al començar        
        data.bulletsInMagazine.RuntimeValue = data.bulletsInMagazine.InitialValue; 
    }


}
