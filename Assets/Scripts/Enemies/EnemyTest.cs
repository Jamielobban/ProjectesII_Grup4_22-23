using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public float shootRange;
    public float moveRange;

    public int dispersion;
    public int consecucion;

    public float moveSpeed;
    public float shootSpeed;
    public float reloadTime;

    private Transform player;
    private Transform playerPos;
    public Transform weapon;
    public Transform spawn;

    private Vector3 targetPoint;
    private Quaternion targetRotation;


    private SpriteRenderer sr;
    public GameObject bullet;
    bool reloading;
    public bool damage;
    float weaponPosX = 0.5f, weaponPosZ = 0;
    enum States { MOVEMENT, SHOOT };
    States enemyState;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //player = GameObject.FindGameObjectWithTag("Player");
        enemyState = States.MOVEMENT;
        reloading = false;

        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (damage)
        {
            GetDamage();
            damage = false;
        }

        playerPos = player.transform;

        switch (enemyState)
        {
            case States.MOVEMENT:
                {
                    MoveToRangePosition();
                }
                break;
            case States.SHOOT:
                {
                    shoot();
                }
                break;
        }
    }

   
    void GetDamage()
    {
        sr.color = new Color(0.5f, 0, 0, 1);
        StartCoroutine(daño(0.1f));
    }
    private IEnumerator daño(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }
    void shoot()
    {

        for (int i = 0; i < consecucion; i++)
        {
            StartCoroutine(shoot(0.2f * i));
        }

        reloading = true;
        enemyState = States.MOVEMENT;
        StartCoroutine(reload(reloadTime));

    }
    private IEnumerator reload(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        reloading = false;

    }

    private IEnumerator shoot(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        int par = 0;
        if (dispersion % 2 == 0)
            par = 5;


        for (int i = 0, grados = ((dispersion / 2) * -10) + par; i < dispersion; i++, grados += 10)
        {
            GameObject instance = Instantiate(bullet, spawn.transform.position, spawn.rotation);
            instance.transform.Rotate(0, 0, instance.transform.rotation.z + grados);

            instance.GetComponent<Rigidbody2D>().AddForce(-instance.transform.up * shootSpeed * Time.timeScale);
            Destroy(instance, 3);
        }
    }

    void MoveToRangePosition()
    {
        var step = moveSpeed * Time.deltaTime;

        //Apuntado
        Vector3 dir = playerPos.position - weapon.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        weapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        //Posicion del jugador respecto al enemigo para cambiar el arma de lado
        Vector3 dirPlayer = playerPos.position - transform.position;
        float anglePlayer = Mathf.Atan2(dirPlayer.y, dirPlayer.x) * Mathf.Rad2Deg;

        weapon.transform.localPosition = new Vector3(weaponPosX, -0.3f, weaponPosZ);

        if (anglePlayer > 90 || anglePlayer < -90)
        {
            weaponPosX = -0.5f;
        }
        else
        {
            weaponPosX = 0.5f;

        }

        if (angle < 0)
        {
            weaponPosZ = -0.5f;
        }
        else
        {
            weaponPosZ = 0.5f;
        }


        if (Vector3.Distance(transform.position, playerPos.position) >= moveRange + 1f)
        {

            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, step);

        }
        else if (Vector3.Distance(transform.position, playerPos.position) <= moveRange - 1f)
        {

            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, -step);

        }


        if (Vector3.Distance(transform.position, playerPos.position) <= shootRange && !reloading)
        {

            enemyState = States.SHOOT;

        }
    }
}
