using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaBullet : Bullet{

    [SerializeField]
    GameObject blackHole;
   

    
    private float timePassed;
    private int multiplier;


    public GameObject originBullet;

    public float m_RotationSpeed;
    //private RelativeJoint2D relativeJoint;
    float lastTimeEnter;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        //bulletDamage = 30 * _damageMultiplier;
        //bulletRangeInMetres = 60;
        //bulletSpeedMetresPerSec = 15; //20
        //bulletRadius = 0.23f;

        timePassed = 1;
        rb = this.GetComponent<Rigidbody2D>();
        multiplier = 0;
        //relativeJoint = GetComponent<RelativeJoint2D>();
        //relativeJoint.connectedBody = originBullet.GetComponent<Rigidbody2D>();
        lastTimeEnter = Time.time;
        //if (powerUpOn)
        //{           
        //    relativeJoint.attachedRigidbody.inertia = 0.5f;   
        //}       
    }
    // Update is called once per frame     
    private void FixedUpdate()
    {       
        if (powerUpOn)
        {
            Vector2 normalizedVel = rb.velocity.normalized;
            float waveVariation1 = 0.8f * Mathf.Sin(timePassed * 20 + Mathf.PI * multiplier);//float waveVariation1 = 0.8f * Mathf.Sin(Time.time * rb.velocity.magnitude + Mathf.PI);
            float waveVariation2 = 0.8f * Mathf.Sin(timePassed * 20 + 2 * Mathf.PI * multiplier);//float waveVariation2 = 0.8f * Mathf.Sin(Time.time * rb.velocity.magnitude + 2*Mathf.PI);
            transform.position = new Vector3(transform.position.x + normalizedVel.y * waveVariation2, transform.position.y + normalizedVel.x * waveVariation1, transform.position.z);

            timePassed += Time.deltaTime;
            multiplier = 1;
        }
        else
        {            
            if(Time.time - lastTimeEnter >= 0.01)
            {
                //relativeJoint.angularOffset += (360/100f);
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
        //Debug.Log(bulletDamage);
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
                bulletInfo.damage = bulletData.bulletDamage;
                bulletInfo.impactPosition = transform.position;
                collision.gameObject.GetComponent<Entity>().GetDamage(bulletData.bulletDamage, HealthStateTypes.NORMAL, 0, this.transform.position);
                base.HitSomeone();
                //Debug.Log("NOPU");

            }
        }      


    }
    
}
