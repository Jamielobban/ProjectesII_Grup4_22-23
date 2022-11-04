using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sniper : Weapon
{
    
    public Sniper(Transform _firePoint, ref SpriteRenderer _sr) :base(_firePoint,ref _sr) {   
        data.bulletsPerMagazine = Random.Range(4,9);
        data.magazines = Random.Range(2, 4);       
        data.reloadTimeInSec = 3.8f;
        data.currentBulletsInMagazine = data.bulletsPerMagazine;
        data.currentMagazines = data.magazines;
        data.fireRateinSec = Random.Range(100f, 200f); //Aqui esta en dpm
        data.weaponSprite = Resources.Load<Sprite>("Assets/Sprites/Square.png");        
        data.bulletTypePrefab = Resources.Load<GameObject>("Assets/SniperBullet.prefab");
    }

    protected override void CheckPowerUpShooting()
    {
        data.bulletTypePrefab.GetComponent<Bullet>().powerUpOn = true;
        data.mechanism.Shoot(data.bulletTypePrefab, data.firePoint, data.fireRateinSec);
    }

    public override void Update()
    {
        base.Update();
    }  
    
}
 