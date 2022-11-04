using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{

    private enum State
    {
        Chasing, Firing, Hit
    }
    private State state;

    private float enemyHealth;
    public float bulletSpeed;

    public float timeBetweenShots;
    public float lastShoot;

    public Color originalColor;

    public Transform enemyFirePoint;
    public GameObject enemyBulletPrefab;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;


    private Transform player;

    public bool canShoot;

    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr;

    public bool canSeePlayer;
    public bool inRange;
    public float knockbackPower;

    public int angleOfCone;
    public int numberOfBursts;
    void Start()
    {
        enemyHealth = -1;
        state = State.Chasing;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        lastShoot = 0;
    }


    void Update()
    {
        //if (enemyHealth <= 0)
        //{
        //    DeathAnimation();
        //}

        // Debug.Log("am i working");
        switch (state)
        {
            case State.Chasing:

                if (inRange)
                {
                    state = State.Firing;
                }
                break;
            case State.Firing:
                RaycastHit2D hitInfo = Physics2D.Raycast(enemyFirePoint.position, transform.right);
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.CompareTag("Player"))
                    {
                        Debug.DrawRay(enemyFirePoint.position, hitInfo.point, Color.green);
                        canShoot = true;
                    }
                    if (hitInfo.collider.CompareTag("MapLimit"))
                    {
                        Debug.DrawRay(enemyFirePoint.position, hitInfo.point, Color.red);
                        canShoot = false;
                        lastShoot = Time.time;
                    }
                }
                if (!inRange)
                {
                    state = State.Chasing;
                }
                else if ((Time.time - lastShoot >= timeBetweenShots) && canShoot)
                {
                    Shoot();
                    lastShoot = Time.time;
                }
                break;
            case State.Hit:
                if (enemyHealth <= 0)
                {
                    DeathAnimation();
                }
                sr.DOColor(originalColor, 0.6f);
                //rb.AddForce(transform.right * -knockbackPower, ForceMode2D.Impulse);
                lastShoot = Time.time;
                //state = State.Chasing;
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        switch (state)
        {
            case State.Chasing:


                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
                    inRange = false;
                }
                else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
                {
                    transform.position = this.transform.position;
                    inRange = true;
                }
                break;
            case State.Firing:
                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    inRange = false;
                }
                else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.fixedDeltaTime);
                    inRange = true;
                }
                break;
            case State.Hit:
                break;
            default:
                break;
        }
    }

    void Shoot()
    {

        for (int i = 0; i < numberOfBursts; i++)
        {
            StartCoroutine(Load(0.2f * i));
        }

        //reloading = true;
        //enemyState = States.MOVEMENT;
        //StartCoroutine(reload(reloadTime));

    }

    private IEnumerator Load(float waitTime)
    {
        if (inRange)
        {
            //GameObject enemyBullet = Instantiate(enemyBulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
            //Rigidbody2D rbBullet = enemyBullet.GetComponent<Rigidbody2D>();
            //rbBullet.AddForce(enemyFirePoint.right * bulletSpeed, ForceMode2D.Impulse);
            //rb.AddForce(transform.right * -knockbackPower, ForceMode2D.Impulse);



            yield return new WaitForSeconds(waitTime);
            int par = 0;
            if (angleOfCone % 2 == 0)
                par = 5;


            for (int i = 0, grados = ((angleOfCone / 2) * -10) + par; i < angleOfCone; i++, grados += 10)
            {
                GameObject instance = Instantiate(enemyBulletPrefab, enemyFirePoint.transform.position, enemyFirePoint.rotation);
                instance.transform.Rotate(0, 0, instance.transform.rotation.z + grados);

                instance.GetComponent<Rigidbody2D>().AddForce(instance.transform.right * bulletSpeed, ForceMode2D.Impulse);
                Destroy(instance, 3);
            }
        }
    }

    void DeathAnimation()
    {
        //rb.AddForce(transform.right * -knockbackPower, ForceMode2D.Impulse);
        Invoke("DestroyGameObject", 2f);
        sr.DOColor(Color.clear, 2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log(("ive been hit"));
            //enemyHealth -= 20;
            //sr.DOColor(Color.red, 0.0f);
            //state = State.Hit;
            StartCoroutine(hitEnemy());
        }
    }

    private void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }
    private IEnumerator hitEnemy()
    {
        state = State.Hit;
        rb.AddForce(transform.right * -knockbackPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(2);
        enemyHealth -= 20;
        sr.DOColor(Color.red, 0.0f);
        state = State.Chasing;
    }
}
