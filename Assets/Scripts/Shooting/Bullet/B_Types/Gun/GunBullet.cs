using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Bullet
{
    private Rigidbody2D rb;   
    Transform originalFirePoint;
    
    protected override void Start()
    {
        base.Start();

        bulletDamage = 20*_damageMultiplier;
        bulletRangeInMetres = 100;
        bulletSpeedMetresPerSec = 20;
        bulletRadius = 0.23f;

        rb = this.GetComponent<Rigidbody2D>();

        originalFirePoint = this.transform;
        //originalFirePoint.Rotate(0, 0, originalFirePoint.transform.rotation.z + Random.Range(-15, 15));
        rb.AddForce(originalFirePoint.up * -bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        //Debug.Log(rb.velocity);
        //originalVector = rb.velocity;
        
        
    }

    protected override void Update()
    {
        base.Update();
        //this.transform.Rotate(0, 0, transform.rotation.z*Mathf.Sin(Time.time*4)*15);
        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        //originalFirePoint.Rotate(0, 0, originalFirePoint.transform.rotation.z + Random.Range(-15, 15));
        //originalVector = transform.position + Time.time * originalFirePoint.up;

        //Vector3 scaledVector = originalVector * Mathf.Sin(Time.time*0.5f)*3f;
        //this.transform.position = scaledVector;
        //transform.position = originalFirePoint.position + Time.time * originalVector;
        //transform.position = originalPosition + rb.velocity.normalized * Time.time;       
        //rb.velocity *= originalFirePoint.up * Mathf.Sin(Time.time)*10;
        //transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time*4)*2, transform.position.z);       
        //transform.Rotate(transform.position*Mathf.Sin(Time.time*4)*2);
        //rb.velocity *= rb.rotation;

        //float frequency = 1.5f;
        //float phase = 0;

        //Debug.Log(angle);
        //transform.position = new Vector3(transform.position.x + Mathf.Cos(angle), transform.position.y + Mathf.Sin(angle),transform.position.z);


        //transform.localPosition = new Vector3(transform.localPosition.x + Mathf.Cos(Time.time * 30f) * 0.04f, transform.localPosition.y + Mathf.Sin(Time.time * 30f) * 0.04f, transform.localPosition.z);
        //float k = 2 * Mathf.PI / waveLenght;
        //float w = k / rb.velocity.magnitude;

        //transform.position = new Vector3(transform.position.x, A * Mathf.Cos(w * Time.time - k * transform.position.x), transform.position.z);

        //y = A * cos(wt - kx)

        //w = k/v

        //k = 2 * glm::pi<float>() / wavelength;

        //v = wavelength / T
        //Vector3 normalizedVel = rb.velocity.normalized;
        ////transform.Rotate(normalizedVel,  angle);
        ////transform.Rotate()
        ////float perpendicularAVel = -normalizedVel.y / normalizedVel.x;
        //incrementoEnY = Mathf.Sin(Time.time)-transform.position.y;
        //incrementoEnX = Mathf.Cos(Time.time)-transform.position.x;


        //transform.position += new Vector3(0, Mathf.Sin(Mathf.Sin(angle) * Time.time), 0);

        //x = transform.position.x + rb.velocity.x * Time.deltaTime;
        //y = Mathf.Sin(x * 2) * 3;
        //transform.position = new Vector2(x, y);

        //transform.position = new Vector3(transform.position.x, transform.position.y + incrementoEnY*perpendicularAVel, 0);

        //posX = Mathf.Cos(transform.position.x);

        //cada 2 puja 1        
        
        
        
    }
    private void FixedUpdate()
    {
        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        //float waveVariation =  0.2f * Mathf.Sin(Time.time * 40);
        //transform.position = new Vector3(transform.position.x + Mathf.Sin(angle) * waveVariation, transform.position.y + Mathf.Cos(angle) * waveVariation, transform.position.z);
        Vector2 normalizedVel = rb.velocity.normalized;
        float waveVariation1 = 0.8f *  Mathf.Sin(Time.time*rb.velocity.magnitude);
        float waveVariation2 = 0.8f *  Mathf.Sin(Time.time*rb.velocity.magnitude + Mathf.PI);
        transform.position = new Vector3(transform.position.x + normalizedVel.y * waveVariation2, transform.position.y + normalizedVel.x * waveVariation1, transform.position.z);

        


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
            base.ImpactBody();
            bulletInfo.damage = bulletDamage;
            bulletInfo.impactPosition = transform.position;
            collision.gameObject.SendMessage("GetDamage", bulletInfo);
        }
    }


    //private void HitSomeone()
    //{
    //    Instantiate(collisionEffect, transform.position, Quaternion.identity);
    //}

}
