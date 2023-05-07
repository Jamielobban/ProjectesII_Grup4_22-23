using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SniperLineBullet : Bullet
{


    private LineRenderer bullet;
    [SerializeField]
    public LayerMask mapLimit;

    private RecoilScript _recoil;

    private GameObject player;

    bool charge;

    public GameObject shootBullet;
    public GameObject shootPowerUpBullet;

    public Animator muzzleShoot;

    public AudioClip shootSound;
    public GameObject muzzleFlash;
    private GameObject rotatePoint;
    int? shootSoundKey;
    void Start()
    {
        _recoil = FindObjectOfType<RecoilScript>();
        rotatePoint = GameObject.FindGameObjectWithTag("PlayerFirePoint");

        if (powerUpOn)
        {
            StartCoroutine(powerUpShoot(1.2f));

            muzzleShoot.speed = 0.6f;
        }
        else
        {
            StartCoroutine(normalShoot(0.6f));

            muzzleShoot.speed = 1.2f;
        }

        player = GameObject.FindGameObjectWithTag("Player");

        this.transform.SetParent(GameObject.FindGameObjectWithTag("RotatePoint").transform);

        bullet = GetComponent<LineRenderer>();
      

        charge = true;
    }

    private IEnumerator powerUpShoot(float time)
    {

        yield return new WaitForSeconds(time);
        charge = false;



        GameObject shoot = GameObject.Instantiate(shootPowerUpBullet, transform.position, transform.rotation);

        shoot.GetComponent<Bullet>().ApplyMultiplierToDamage(GetDamageMultiplier());
        shoot.GetComponent<Bullet>().FireProjectile(/*this.transform*/);

        Destroy(this.gameObject,0.3f);

    }


    
    private IEnumerator normalShoot(float time)
    {

        yield return new WaitForSeconds(time);
        player.GetComponent<PlayerMovement>().canMove = true;
        charge = false;

        shootSoundKey = AudioManager.Instance.LoadSound(shootSound, player.transform);


        GameObject shoot = GameObject.Instantiate(shootBullet, transform.position, transform.rotation);
        _recoil.AddRecoil();
        Instantiate(muzzleFlash, rotatePoint.transform.position, rotatePoint.transform.rotation);

        shoot.GetComponent<Bullet>().ApplyMultiplierToDamage(GetDamageMultiplier());
        shoot.GetComponent<Bullet>().FireProjectile(/*this.transform*/);

        Destroy(this.gameObject, 0.3f);
    }


    void Update()
    {


        if (charge)
        {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, -transform.up, 500, mapLimit);
            Ray r = new Ray(player.transform.position, -transform.forward);

            bullet.SetPosition(0, transform.position);
        bullet.SetPosition(1, transform.position -transform.up * 30);

        }
        else
        {       
            bullet.positionCount = 0;
        }




    }







}