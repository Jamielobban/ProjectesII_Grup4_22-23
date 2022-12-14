using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject[] points;
    public float velocity;
    //Cuando llega al final vuelve al principio
    public bool loop;
    int nextPosition;
    bool direction;
    public float waitTime;
    float currentWaitTime;
    // Start is called before the first frame update
    void Start()
    {
        currentWaitTime = Time.realtimeSinceStartup;
        nextPosition = 0;
        transform.position = points[nextPosition].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(points[nextPosition].transform.position, transform.position) < 0.01f)
        {
            if (loop)
            {
                nextPosition = (nextPosition + 1) % points.Length;
                currentWaitTime = Time.realtimeSinceStartup;
            }
            else
            {
                if (direction && nextPosition + 1 == points.Length)
                {
                    direction = false;
                }
                else if (!direction && nextPosition - 1 == -1)
                {
                    direction = true;
                }

                if (direction)
                    nextPosition++;
                else
                    nextPosition--;
                currentWaitTime = Time.realtimeSinceStartup;

            }
        }

        if (currentWaitTime + waitTime < Time.realtimeSinceStartup)
        {
            transform.position = Vector3.MoveTowards(transform.position, points[nextPosition].transform.position, velocity);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(GameObject.FindGameObjectWithTag("PlayerContain").transform);
        }
    }
}
