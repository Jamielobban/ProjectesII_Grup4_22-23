using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : Weapon
{
    protected GameObject secondHandPrefab;
    protected GameObject secondHandClone;
    protected Mechanism temporalMechanism;
    public Gun(Transform _firePoint) : base(_firePoint)
    {
        data.bulletsPerMagazine = Random.Range(13, 20);
        data.magazines = Random.Range(1, 2); // 4/7
        data.reloadTimeInSec = 2f;
        data.maxTimeOnPowerup = 8f;
        data.currentBulletsInMagazine = data.bulletsPerMagazine;
        data.currentMagazines = data.magazines;
        data.fireRateinSec = Random.Range(660f, 700f); //Aqui esta en dpm
        data.weaponSprite = Resources.Load<Sprite>("Sprites/Pistol"); 
        data.reloadSound = Resources.Load<AudioClip>("Sounds/Weapons/Pistol/gunreload");
        data.bulletTypePrefab = Resources.Load<GameObject>("Prefab/GunBullet");
        data.amplitudeGain = 0f;
        secondHandPrefab = Resources.Load<GameObject>("Prefab/LeftHand");
    }

    
    protected override void CheckPowerUpShooting()
    {        
        //No gastar municio

        data.mechanism.Shoot(data.bulletTypePrefab, data.firePoint, data.fireRateinSec, data.shootSound, data.amplitudeGain, data.damageMultiplier);     


    }

    public override void Update()
    {
        base.Update();

        if (Time.time - data.timelastPowerupEnter >= data.maxTimeOnPowerup && data.powerActive)
        {
            data.powerActive = false;
            GameObject.Destroy(secondHandClone.gameObject);
            
            data.powerupAvailable = false;                    
            data.timelastPowerupExit = Time.time;
            AudioManager.Instance.PlaySound(powerupEmpty);
        }
    }

    protected override void ActionOnEnterPowerup()
    {
        //Fer duales 
        secondHandClone = GameObject.Instantiate(secondHandPrefab);
        secondHandClone.GetComponent<SpriteRenderer>().sprite = data.weaponSprite;
        secondHandClone.GetComponent<SpriteRenderer>().color = data.weaponColor;
        secondHandClone.GetComponent<LeftHand>().lifeTime = data.maxTimeOnPowerup;
        secondHandClone.GetComponent<LeftHand>().weaponInHand = this;
        //Recarga automatica al començar        
        data.currentBulletsInMagazine = data.bulletsPerMagazine; 
    }


}
