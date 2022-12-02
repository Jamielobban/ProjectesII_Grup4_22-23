using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBewteenSpawns;
    public float startTimeBetweenSpawns;

    public GameObject echo;
    private PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (player.isMoving)
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
