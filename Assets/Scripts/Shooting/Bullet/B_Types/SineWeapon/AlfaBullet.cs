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
    private RelativeJoint2D relativeJoint;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        //Debug.Log(rb.velocity);


        bulletDamage = 10 * _damageMultiplier;
        bulletRangeInMetres = 60;
        bulletSpeedMetresPerSec = 15; //20
        bulletRadius = 0.23f;
        timePassed = 1;
        multiplier = 0;
        relativeJoint = GetComponent<RelativeJoint2D>();
        rb = this.GetComponent<Rigidbody2D>();

        //relativeJoint.connectedBody = originBullet.GetComponent<Rigidbody2D>();
        if (powerUpOn)
        {
            relativeJoint.attachedRigidbody.inertia = 0.5f;
        }
        //{            
        //    relativeJoint.enabled = true;            
        //    
        //}
        //else
        //{            
        //    relativeJoint.enabled = false;
        //}

    }
    // Update is called once per frame    
    private void FixedUpdate()
    {
        //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        //float waveVariation =  0.2f * Mathf.Sin(Time.time * 40);
        //transform.position = new Vector3(transform.position.x + Mathf.Sin(angle) * waveVariation, transform.position.y + Mathf.Cos(angle) * waveVariation, transform.position.z);

        if (!powerUpOn)
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

            //transform.RotateAround(Camera.main.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.Follow.position, Vector3.forward, 20);
            //Debug.Log(Camera.main.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.Follow.position);
            relativeJoint.angularOffset += (720*Time.deltaTime);
            
        }


        //

        //Transform _pivot;
        //Vector3 _offsetDirection;
        //float _distance;

        //public void SetPivot(Transform pivot)
        //{
        //    if (pivot != null)
        //    {
        //        _pivot = pivot;
        //        _offsetDirection = transform.position - pivot.position;
        //        _distance = _offsetDirection.magnitude;
        //    }
        //    else
        //    {
        //        _pivot = null;
        //    }
        //}

        //void Update()
        //{
        //    if (_pivot == null) return;

        //    Quaternion rotate = Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime);

        //    _offsetDirection = (rotate * _offsetDirection).normalized;

        //    transform.position = _pivot.position + _offset * _distance;
        //}
    }

    //float waveInPiAngle= 1f * Mathf.Sin(Time.time*10 );
    //float waveVariationForAngle = 1 - Mathf.Sin(angle);

    ////float waveVariation = 0.8f *  Mathf.Sin(Time.time*30);

    //transform.tr
    //transform.position = new Vector3(Mathf.Sin(angle) * waveVariation, Mathf.Cos(angle)*waveVariation, transform.position.z);


    protected override void Update()
    {
        if (!powerUpOn)
        {
            base.Update();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!powerUpOn)
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
        else
        {

        }
        


    }
}


