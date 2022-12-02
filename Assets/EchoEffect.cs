using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBewteenSpawns;
    public float startTimeBetweenSpawns;

    public GameObject echo;
    private Saw thisSaw;

    private void Start()
    {
        thisSaw = GetComponent<Saw>();
    }

    private void Update()
    {
        if (thisSaw.isAlive)
        {
            if(timeBewteenSpawns <= 0)
            {
                GameObject instance = (GameObject)Instantiate(echo, transform.position, Quaternion.identity);
                Destroy(instance, 2f);
                timeBewteenSpawns = startTimeBetweenSpawns;
            }
            else
            {
                timeBewteenSpawns -= Time.deltaTime;
            }
        }
    }
}
