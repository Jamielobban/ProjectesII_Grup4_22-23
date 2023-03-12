using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEnemie : MonoBehaviour
{
    public GameObject enemies;
    public float distanceToTeleport;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(teleport());
    }
    private IEnumerator teleport()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < enemies.transform.childCount; i++)
        {
            if (enemies.transform.GetChild(0) != null)
            {
                if(Vector3.Distance(enemies.transform.GetChild(0).transform.position, this.transform.position) > distanceToTeleport)
                {
                    enemies.transform.GetChild(0).transform.position = this.transform.position;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
