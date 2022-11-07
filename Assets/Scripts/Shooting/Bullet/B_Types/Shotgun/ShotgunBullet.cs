using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : Bullet
{
    GameObject[] pelletsOnBullet = new GameObject[5];
    GameObject pelletPrefab;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start();

        bulletDamage = 50;
        bulletRangeInMetres = 10;
        bulletSpeedMetresPerSec = 20;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();        

        pelletPrefab = Resources.Load<GameObject>("Prefab/Pellet");

        if (powerUpOn)
        {
            pelletPrefab.GetComponent<Pellet>().powerUpOn = true;
        }
        else
        {
            pelletPrefab.GetComponent<Pellet>().powerUpOn = false;
        }

        for (int i = 0, grados = -20; i < pelletsOnBullet.Length;i++,grados+=10){
            pelletsOnBullet[i] = GameObject.Instantiate(pelletPrefab, transform.position, transform.rotation);
            pelletsOnBullet[i].transform.Rotate(0, 0, pelletsOnBullet[i].transform.rotation.z + grados);

            //Transform originalFirePoint = this.transform;
            //rb.AddForce(originalFirePoint.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
            pelletsOnBullet[i].GetComponent<Rigidbody2D>().AddForce(pelletsOnBullet[i].transform.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);

            if (powerUpOn)
            {
                pelletsOnBullet[i].GetComponent<Pellet>().powerUpOn = true;
            }
            else
            {
                pelletsOnBullet[i].GetComponent<Pellet>().powerUpOn = false;
            }
        }
        Destroy(this.gameObject);
    }

    protected override void Update()
    {
        base.Update();
    }
}
