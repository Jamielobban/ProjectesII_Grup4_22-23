using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetetctorArea : MonoBehaviour
{
    [HideInInspector]
    public bool playerInside = false;
    [SerializeField]
    int enemyNumber = 0;
    
    private void Update()
    {
        switch (enemyNumber)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                GetComponentInParent<Enemy4>().inRange = playerInside;
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                GetComponentInParent<Enemy8>().inRange = playerInside;
                break;
            case 9:
                //GetComponentInParent<Enemy9>().inRange = playerInside;
                break;
            case 10:
                break;
        }
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;
            
        }
            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = false;
    }
}
