using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamesRange : MonoBehaviour
{    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<Enemy5>().longRange = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponentInParent<Enemy5>().longRange = true;
        }
    }   
}
