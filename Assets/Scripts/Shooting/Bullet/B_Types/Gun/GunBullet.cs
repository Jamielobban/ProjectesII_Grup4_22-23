using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : Bullet
{
    private PlayerMovement playerpos;
    private RecoilScript _recoil;
    public Turn will;
    private GameObject rotatePoint;
    public GameObject muzzleFlash;
    public GameObject bulletShell;

    protected override void Start()
    {
        base.Start();

        rotatePoint = GameObject.FindGameObjectWithTag("PlayerFirePoint");
        playerpos = FindObjectOfType<PlayerMovement>();
        _recoil = FindObjectOfType<RecoilScript>();
        will = FindObjectOfType<Turn>();

        Instantiate(muzzleFlash, rotatePoint.transform.position, rotatePoint.transform.rotation);
        Instantiate(bulletShell, will.transform.position, Quaternion.identity);


        rb = this.GetComponent<Rigidbody2D>();
        bulletData.bulletDamage *= bulletData._damageMultiplier;

        _recoil.AddRecoil();

        playerpos.knockback = true;
        playerpos.rb.velocity = new Vector2((-rb.velocity.x * 0.1f), (-rb.velocity.y * 0.1f));



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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit"))
        {
            base.ImpactWall();            
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            base.ImpactBody();
            collision.gameObject.GetComponent<Entity>().GetDamage(bulletData.bulletDamage, HealthStateTypes.NORMAL, 0, this.transform.position, TransformMovementType.PUNCH);

        }
    }


    //private void HitSomeone()
    //{
    //    Instantiate(collisionEffect, transform.position, Quaternion.identity);
    //}

}
