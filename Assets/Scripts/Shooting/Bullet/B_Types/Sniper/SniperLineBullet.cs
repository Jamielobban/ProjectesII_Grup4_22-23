using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SniperLineBullet : MonoBehaviour
{


    private LineRenderer bullet;
    [SerializeField]
    public LayerMask mapLimit;

 
    private GameObject player;

    bool charge;

    public GameObject shootBullet;
    public float damageMultiplier;

    public AudioClip shootSound;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        this.transform.SetParent(GameObject.FindGameObjectWithTag("RotatePoint").transform);

        bullet = GetComponent<LineRenderer>();
      
        player.GetComponent<PlayerMovement>().canMove = false;

        charge = true;
        StartCoroutine(chargeTime(0.6f));
    }


    private IEnumerator chargeTime(float time)
    {

        yield return new WaitForSeconds(time);
        player.GetComponent<PlayerMovement>().canMove = true;
        charge = false;
        this.transform.SetParent(null);


        AudioManager.Instance.PlaySound(shootSound);

        GameObject shoot = GameObject.Instantiate(shootBullet, transform.position, transform.rotation);

        shoot.GetComponent<Bullet>().ApplyMultiplierToDamage(damageMultiplier);

        Destroy(this.gameObject);
    }


    void Update()
    {

        if(charge)
        {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, -transform.up, 500, mapLimit);

        bullet.SetPosition(0, transform.position);
        bullet.SetPosition(1, hitInfo.point);

        }
        else
        {       
            bullet.positionCount = 0;
        }


    }







}