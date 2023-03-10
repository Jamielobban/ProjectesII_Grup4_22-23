using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<GameObject>  points;
    public float[] waitsTime;
    public float velocity;
    //Cuando llega al final vuelve al principio
    public bool loop;
    public int nextPosition;
    public bool direction;
    float currentWaitTime;

    public float waitTime;
    bool start;
    // Start is called before the first frame update
    void Start()
    {
        start = true;
        currentWaitTime = Time.time;
        transform.position = points[nextPosition].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(points[nextPosition].transform.position, transform.position) < 0.01f && !start)
        {
            if (loop)
            {
                nextPosition = (nextPosition + 1) % points.Count;
                currentWaitTime = Time.time;
            }
            else
            {
                if (direction && nextPosition + 1 == points.Count)
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
                currentWaitTime = Time.time;

            }
        }

        if(start)
        {
            if (waitsTime.Length == 0)
            {
                if (currentWaitTime + waitTime < Time.time)
                {
                    start = false;
                }
            }
            else
            {
                int a;
                if (direction)
                    a = -1;
                else
                    a = 1;

                if (currentWaitTime + waitsTime[nextPosition+a] < Time.time)
                {
                    start = false;

                }
            }
        }

        if(waitsTime.Length == 0)
        {
            if (currentWaitTime + waitTime < Time.time)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[nextPosition].transform.position, velocity*Time.deltaTime);

            }
        }
        else
        {
            int a;
            if (direction)
                a = -1;
            else
                a = 1;
            if (currentWaitTime + waitsTime[nextPosition+a] < Time.time)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[nextPosition].transform.position, velocity * Time.deltaTime);

            }
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
