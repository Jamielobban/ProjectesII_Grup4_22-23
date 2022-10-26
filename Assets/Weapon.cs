using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{  
    public virtual void Shoot()
    {

    }

    protected int bulletsPerMagazine;
    protected int Magazines;   
    protected float timeLastShoot;
    protected bool reloading;

    Bullet weaponBulletType;







    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
