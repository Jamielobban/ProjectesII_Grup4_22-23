using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDistanceCheck : MonoBehaviour
{
    [SerializeField]
    private ElectricConnection ec;

    public bool isInRange;
    public bool justLeft;
    void Start()
    {
        isInRange = false;
        justLeft = false;
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
            justLeft = true;
        }
    }
}
