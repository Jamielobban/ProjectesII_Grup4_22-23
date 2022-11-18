using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineBullet : Bullet
{
    //private Rigidbody2D rb;
    //Transform originalFirePoint;    
    //[SerializeField]
    //GameObject betaBullet;
    //[SerializeField]
    //GameObject alfaBullet;
    //private Dictionary<int, Vector3> enemiesInfo = new Dictionary<int, Vector3>();

    private Rigidbody2D rb;
    Transform originalFirePoint;
    [SerializeField]
    GameObject betaBullet;
    [SerializeField]
    GameObject alfaBullet;
    [SerializeField]
    GameObject betaBulletChild;
    [SerializeField]
    GameObject alfaBulletChild;
    [SerializeField]
    float ballsSpeed;
    private Dictionary<int, Vector3> enemiesInfo = new Dictionary<int, Vector3>();
    float lastEnter;
    float counter;
    

    protected override void Start()
    {       

        base.Start();

        lastEnter = 0; 
        bulletRangeInMetres = 90;//6s
        bulletSpeedMetresPerSec = 15;//20
        bulletRadius = 0.23f;        
        counter = 0;
        rb = this.GetComponent<Rigidbody2D>();

        originalFirePoint = this.transform;
        //originalFirePoint.Rotate(0, 0, originalFirePoint.transform.rotation.z + Random.Range(-15, 15));

        //Debug.Log(rb.velocity);
        //originalVector = rb.velocity;



        if (powerUpOn)
        {
            rb.AddForce(originalFirePoint.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);

            betaBullet.GetComponent<BetaBullet>().powerUpOn = true;
            alfaBullet.GetComponent<AlfaBullet>().powerUpOn = true;

            GameObject betaBulletClone = GameObject.Instantiate(betaBullet, transform.position, transform.rotation);
            betaBulletClone.GetComponent<BetaBullet>().originBullet = this.gameObject;
            betaBulletClone.GetComponent<Rigidbody2D>().AddForce(betaBulletClone.transform.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);

            GameObject alfaBulletulletClone = GameObject.Instantiate(alfaBullet, transform.position, transform.rotation);
            alfaBulletulletClone.GetComponent<AlfaBullet>().originBullet = this.gameObject;
            alfaBulletulletClone.GetComponent<Rigidbody2D>().AddForce(alfaBulletulletClone.transform.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 1);
            alfaBulletChild.GetComponent<AlfaBullet>().powerUpOn = false;
            betaBulletChild.GetComponent<BetaBullet>().powerUpOn = false;
            betaBulletChild.GetComponent<BetaBullet>().originBullet = this.gameObject;
            betaBulletChild.GetComponent<BetaBullet>().ApplyMultiplierToDamage(1);
            alfaBulletChild.GetComponent<AlfaBullet>().originBullet = this.gameObject;
            alfaBulletChild.GetComponent<AlfaBullet>().ApplyMultiplierToDamage(1);
            alfaBulletChild.SetActive(true);
            betaBulletChild.SetActive(true);
        }

    }

    protected override void Update()
    {
        if (!powerUpOn)
        {
            transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;      
            base.Update();
        }
        else
        {
            if (Time.time - timeShooted >= bulletRangeInMetres / bulletSpeedMetresPerSec)
            {
                ResetBalls();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!powerUpOn)
        {
            if(Time.time - lastEnter >= 0.01f)
            {
                counter += ballsSpeed* 360 * 0.01f;                
                transform.rotation = Quaternion.Euler(0, 0, counter);
                lastEnter = Time.time;
                //Debug.Log(counter);
            }

        }
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


    void ResetBalls()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        this.gameObject.SetActive(false);
    }


}



