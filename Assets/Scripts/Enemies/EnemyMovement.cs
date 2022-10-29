using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

    public GameObject projectile;
    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBetweenShots = startTimeBetweenShots;
    }


    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } 
        else if(Vector2.Distance(transform.position,player.position) < stoppingDistance && Vector2.Distance(transform.position,player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        } 
        else if(Vector2.Distance(transform.position,player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }


        if(timeBetweenShots <= 0)
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            Instantiate(projectile, transform.position, Quaternion.identity);
            rb.AddForce(player.up * speed, ForceMode2D.Impulse);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }
}
