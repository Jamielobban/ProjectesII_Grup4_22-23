using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMovement : MonoBehaviour
{
    public float initialSpeed = 20f;
    public float acceleration = 5f;
    public float rotationSpeed = 5f;
    public Transform player;
    public float maxDistance = 10f;

    private Rigidbody2D rb;
    private Vector2 target;
    public bool correctDirection;
    float startTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * initialSpeed, ForceMode2D.Impulse);
        target = player.position;
        startTime = Time.time;
    }

    void Update()
    {
        //correctDirection = Time.time - startTime >= 1;

        //if (!correctDirection)
        //    return;

        //if (Vector2.Distance(transform.position, player.position) > maxDistance)
        //{
        //    target = player.position;
        //}

        //Vector2 direction = (target - (Vector2)transform.position).normalized;
        //rb.AddForce(direction * acceleration * Time.deltaTime);

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle), rotationSpeed * Time.deltaTime);
    }
}
