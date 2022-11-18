using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlfaBullet : Bullet
{
    [SerializeField]
    GameObject blackHole;
    
    private Rigidbody2D rb;    
    private int multiplier;
    public GameObject originBullet;

    private float timePassed;
    
    public float m_RotationSpeed;
    //private RelativeJoint2D relativeJoint;
    float lastTimeEnter;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        //Debug.Log(rb.velocity);


        bulletDamage = 30 * _damageMultiplier;
        bulletRangeInMetres = 60;
        bulletSpeedMetresPerSec = 15; //20
        bulletRadius = 0.23f;
        timePassed = 1;
        multiplier = 0;
        //relativeJoint = GetComponent<RelativeJoint2D>();
        rb = this.GetComponent<Rigidbody2D>();
        lastTimeEnter = Time.time;        
        //if (powerUpOn)
        //{
        //    relativeJoint.attachedRigidbody.inertia = 0.5f;
        //}        

    }        
    private void FixedUpdate()
    {        

        if (powerUpOn)
        {
            Vector2 normalizedVel = rb.velocity.normalized;
            float waveVariation1 = 0.8f * Mathf.Sin(timePassed * 20 * multiplier);//float waveVariation1 = 0.8f * Mathf.Sin(Time.time * rb.velocity.magnitude);
            float waveVariation2 = 0.8f * Mathf.Sin(timePassed * 20 + Mathf.PI * multiplier);//float waveVariation2 = 0.8f * Mathf.Sin(Time.time * rb.velocity.magnitude + Mathf.PI);
            transform.position = new Vector3(transform.position.x + normalizedVel.y * waveVariation2, transform.position.y + normalizedVel.x * waveVariation1, transform.position.z);

            timePassed += Time.deltaTime;
            multiplier = 1;
        }
        else
        {            
            if (Time.time - lastTimeEnter >= 0.01)
            {                
                lastTimeEnter = Time.time;
            }
            
        }

    }    


    protected override void Update()
    {
        if (powerUpOn)
        {
            base.Update();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (powerUpOn)
        {
            if (collision.gameObject.CompareTag("MapLimit"))
            {
                base.ImpactWall();
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                if (originBullet.GetComponent<SineBullet>().CheckBlackHole(collision.gameObject.GetInstanceID(), collision.gameObject.transform.position))
                {
                    GameObject vortex = Instantiate(blackHole, this.transform.position, this.transform.rotation);
                }
                //bulletInfo.damage = bulletDamage;
                //bulletInfo.impactPosition = transform.position;
                //collision.gameObject.SendMessage("GetDamage", bulletInfo);
                base.ImpactBody();                

            }
        }
        else
        {
            if (collision.gameObject.CompareTag("MapLimit"))
            {
                base.HitSomething();
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                bulletInfo.damage = bulletDamage;
                bulletInfo.impactPosition = transform.position;
                collision.gameObject.SendMessage("GetDamage", bulletInfo);
                base.HitSomeone();
                Debug.Log("NOPU");

            }
        }
        


    }
}


