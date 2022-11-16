using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBullet : Bullet
{
    private Rigidbody2D rb;
    Transform originalFirePoint;    
    [SerializeField]
    GameObject betaBullet;
    [SerializeField]
    GameObject alfaBullet;
    private Dictionary<int, Vector3> enemiesInfo = new Dictionary<int, Vector3>();

    protected override void Start()
    {
        base.Start();

        //bulletDamage = 20 * _damageMultiplier;
        bulletRangeInMetres = 60;
        bulletSpeedMetresPerSec = 15;//20
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        originalFirePoint = this.transform;
        //originalFirePoint.Rotate(0, 0, originalFirePoint.transform.rotation.z + Random.Range(-15, 15));
        rb.AddForce(originalFirePoint.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        //Debug.Log(rb.velocity);
        //originalVector = rb.velocity;
        GameObject betaBulletClone = GameObject.Instantiate(betaBullet, transform.position, transform.rotation);
        betaBulletClone.GetComponent<BetaBullet>().originBullet = this.gameObject;
        betaBulletClone.GetComponent<Rigidbody2D>().AddForce(betaBulletClone.transform.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse); 
        GameObject alfaBulletulletClone = GameObject.Instantiate(alfaBullet, transform.position, transform.rotation);
        alfaBulletulletClone.GetComponent<AlfaBullet>().originBullet = this.gameObject;
        alfaBulletulletClone.GetComponent<Rigidbody2D>().AddForce(alfaBulletulletClone.transform.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);        
    }    
    public bool CheckBlackHole(int id, Vector3 position)
    {
        if (enemiesInfo.ContainsKey(id))
        {
            Destroy(this.gameObject);
            return true;
        }
        if(enemiesInfo.Count >= 1)
        {
            Destroy(this.gameObject);
        }
        enemiesInfo.Add(id, position);
        return false;
    }





}

