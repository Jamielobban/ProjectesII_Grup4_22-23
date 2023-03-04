using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetetctorArea : MonoBehaviour
{
    bool playerInside = false;
    private void Update()
    {
        GetComponentInParent<Enemy4>().inRange = playerInside;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            playerInside = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = false;
    }
}
