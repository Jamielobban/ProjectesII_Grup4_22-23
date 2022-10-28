using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyController : MonoBehaviour
{

    private enum State
    {
        Chasing, Firing
    }
    private State state;

    public float bulletSpeed;

    public float timeBetweenShots;
    public float startTimeBetweenShots;

    public Transform enemyFirePoint;
    public GameObject enemyBulletPrefab;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public Color originalColor;
    public Color coolDown;

    EnemyProjectile test;

    private Transform player;


    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer sr;

    void Start()
    {
        state = State.Chasing;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        timeBetweenShots = startTimeBetweenShots;
    }


    void Update()
    {
        switch (state)
        {
            case State.Chasing:
                sr.DOColor(coolDown, 1.9f);
                break;
            case State.Firing:
                GameObject enemyBullet = Instantiate(enemyBulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
                Rigidbody2D rbBullet = enemyBullet.GetComponent<Rigidbody2D>();
                rbBullet.AddForce(enemyFirePoint.right * bulletSpeed, ForceMode2D.Impulse);
                timeBetweenShots = startTimeBetweenShots;
                sr.DOColor(originalColor, 0.1f);
                break;
        }

        if (timeBetweenShots <= 0)
        {
            state = State.Firing;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
            state = State.Chasing;
        }

    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Chasing:
                Vector3 direction = player.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;

                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
                }
                else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
                {
                    transform.position = this.transform.position;
                }
                else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.fixedDeltaTime);
                }
                break;
            case State.Firing:
                break;
        }
        

    }


}
