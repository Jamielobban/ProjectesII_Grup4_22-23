using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaBullet : Bullet{

    [SerializeField]
    GameObject blackHole;       
    private Rigidbody2D rb;
    private float timePassed;
    private int multiplier;
    public GameObject originBullet;    
   
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        bulletDamage = 10 * _damageMultiplier;
        bulletRangeInMetres = 60;
        bulletSpeedMetresPerSec = 15; //20
        bulletRadius = 0.23f;
        timePassed = 1;
        rb = this.GetComponent<Rigidbody2D>();
        multiplier = 0;        
    }

    // Update is called once per frame     
    private void FixedUpdate()
    {
        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        //float waveVariation =  0.2f * Mathf.Sin(Time.time * 40);
        //transform.position = new Vector3(transform.position.x + Mathf.Sin(angle) * waveVariation, transform.position.y + Mathf.Cos(angle) * waveVariation, transform.position.z);
        
        Vector2 normalizedVel = rb.velocity.normalized;
        float waveVariation1 = 0.8f * Mathf.Sin(timePassed * 20 + Mathf.PI * multiplier);//float waveVariation1 = 0.8f * Mathf.Sin(Time.time * rb.velocity.magnitude + Mathf.PI);
        float waveVariation2 = 0.8f * Mathf.Sin(timePassed * 20 + 2 * Mathf.PI * multiplier);//float waveVariation2 = 0.8f * Mathf.Sin(Time.time * rb.velocity.magnitude + 2*Mathf.PI);
        transform.position = new Vector3(transform.position.x + normalizedVel.y * waveVariation2, transform.position.y + normalizedVel.x * waveVariation1, transform.position.z);

        timePassed += Time.deltaTime;
        multiplier = 1;
        
        //float waveInPiAngle= 1f * Mathf.Sin(Time.time*10 );
        //float waveVariationForAngle = 1 - Mathf.Sin(angle);

        ////float waveVariation = 0.8f *  Mathf.Sin(Time.time*30);

        //transform.tr
        //transform.position = new Vector3(Mathf.Sin(angle) * waveVariation, Mathf.Cos(angle)*waveVariation, transform.position.z);

    }

    private void OnTriggerEnter2D(Collider2D collision)
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
            bulletInfo.damage = bulletDamage;
            bulletInfo.impactPosition = transform.position;
            collision.gameObject.SendMessage("GetDamage", bulletInfo);
            base.ImpactBody();
            // Debug.Log(collision.gameObject.transform.position);

        }


    }
}
