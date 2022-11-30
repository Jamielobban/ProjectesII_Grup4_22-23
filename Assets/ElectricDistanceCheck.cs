using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDistanceCheck : MonoBehaviour
{
    [SerializeField]
    private ElectricConnection ec;

    public bool isInRange;
    void Start()
    {
        isInRange = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("FOUND");
            isInRange = true;
            //ec.allEnemies.Add(FindObjectOfType<EnemyController>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("LEFT");
            isInRange = false;
        }
    }
}
