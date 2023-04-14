using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet
{
    GameObject[] pelletsOnBullet = new GameObject[6];
    GameObject[] pelletsOnBulletPU = new GameObject[12];
    GameObject pelletPrefab;
    GameObject powerupPelletPrefab;
    private GameObject bulletShell;
    private GameObject muzzleFlash;
    //public GameObject powerUpPelletCollisions;
    
    //private float _multiplier;
    private Pellet pellet;
    private GameObject firePoint;
    private GameObject rotationPoint;
    protected override void Start()
    {
        base.Start();

        //bulletDamage = 34*_damageMultiplier;
        //bulletRangeInMetres = 5;
        //bulletSpeedMetresPerSec = 20;
        //bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        rotationPoint = GameObject.FindGameObjectWithTag("RotatePoint");
        firePoint = GameObject.FindGameObjectWithTag("PlayerFirePoint");
        pelletPrefab = Resources.Load<GameObject>("Prefab/Pellet");
        bulletShell = Resources.Load<GameObject>("Prefab/BulletParticle");
        powerupPelletPrefab = Resources.Load<GameObject>("Prefab/BigPelletPowerup");
        muzzleFlash = Resources.Load<GameObject>("Prefab/ShotgunMuzzleFlash");

        bulletData.bulletDamage *= bulletData._damageMultiplier;
        CinemachineShake.Instance.ShakeCamera(10f, .1f);

        if (powerUpOn)
        {
            pelletPrefab.GetComponent<Pellet>().powerUpOn = true;
            //pelletPrefab.GetComponent<Pellet>().collisionWallEffect = powerUpPelletCollisions;
        }
        else
        {
            pelletPrefab.GetComponent<Pellet>().powerUpOn = false;
        }

        if (!powerUpOn)
        {
            Instantiate(bulletShell, rotationPoint.transform.position, Quaternion.identity);
            Instantiate(muzzleFlash, firePoint.transform.position, firePoint.transform.rotation);
            for (int i = 0, grados = -80; i < pelletsOnBullet.Length; i++, grados += 10)
            {
                pelletsOnBullet[i] = GameObject.Instantiate(pelletPrefab, transform.position, transform.rotation);
                pelletsOnBullet[i].transform.Rotate(0, 0, pelletsOnBullet[i].transform.rotation.z + Random.Range(-10,10));

                //Transform originalFirePoint = this.transform;
                //rb.AddForce(originalFirePoint.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);

                pelletsOnBullet[i].GetComponent<Rigidbody2D>().AddForce(pelletsOnBullet[i].transform.up * -Random.Range(25, 35)*3, ForceMode2D.Impulse);
                //pelletsOnBullet[i].GetComponent<Bullet>().FireProjectile(originalFirePoint);

                if (powerUpOn)
                {
                    pelletsOnBullet[i].GetComponent<Pellet>().powerUpOn = true;
                    //pelletsOnBullet[i].GetComponent<Pellet>().collisionWallEffect = pellet.powerUpPelletCollisions;
                }
                else
                {
                    pelletsOnBullet[i].GetComponent<Pellet>().powerUpOn = false;
                }
                pelletsOnBullet[i].GetComponent<Pellet>().ApplyMultiplierToDamage(bulletData._damageMultiplier);
            }
            //Destroy(this.gameObject);
        }
        else
        {
            GameObject pelletOnPowerUp = Instantiate(powerupPelletPrefab, transform.position, transform.rotation);
            //pelletOnPowerUp.transform.localScale = new Vector3(0.1f, 0.1f, 1);
            pelletOnPowerUp.GetComponent<Rigidbody2D>().AddForce(pelletOnPowerUp.transform.up * -35, ForceMode2D.Impulse);

            for (int i = 0, grados = -25; i < pelletsOnBulletPU.Length; i++, grados += 5)
            {
                pelletsOnBulletPU[i] = GameObject.Instantiate(pelletPrefab, transform.position, transform.rotation);
                pelletsOnBulletPU[i].transform.Rotate(0, 0, pelletsOnBulletPU[i].transform.rotation.z + grados);

                //Transform originalFirePoint = this.transform;
                //rb.AddForce(originalFirePoint.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);

                pelletsOnBulletPU[i].GetComponent<Rigidbody2D>().AddForce(pelletsOnBulletPU[i].transform.up * -Random.Range(15, 25), ForceMode2D.Impulse);
                //pelletsOnBulletPU[i].GetComponent<Bullet>().FireProjectile(originalFirePoint);


                //GameObject instance = Instantiate(enemyBulletPrefab, enemyFirePoint.transform.position, enemyFirePoint.rotation);
                //instance.GetComponent<EnemyProjectile>().bulletDamage = enemyBulletDamage;
                //AudioManager.Instance.PlaySound(enemyShootSound);
                //instance.transform.Rotate(0, 0, instance.transform.rotation.z + grados);
                //instance.GetComponent<Rigidbody2D>().AddForce(instance.transform.right * bulletSpeed, ForceMode2D.Impulse);

                if (powerUpOn)
                {
                    pelletsOnBulletPU[i].GetComponent<Pellet>().powerUpOn = true;
                }
                else
                {
                    pelletsOnBulletPU[i].GetComponent<Pellet>().powerUpOn = false;
                }
                pelletsOnBulletPU[i].GetComponent<Pellet>().ApplyMultiplierToDamage(bulletData._damageMultiplier);
            }
            //Destroy(this.gameObject);
        }
        
    }

    protected override void Update()
    {
        base.Update();
    }
}
