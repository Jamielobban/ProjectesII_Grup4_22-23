using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricDistanceCheck : MonoBehaviour
{
    [SerializeField]
    private ElectricConnection ec;

    public bool isInRange;
    public bool justLeft;

    private GameObject enemy;
    private bool isntConnected;
    void Start()
    {
        isInRange = false;
        justLeft = false;
        isntConnected = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(("FOUND SEOMTHING"));
        if (collision.CompareTag("Bullet") && !isntConnected)
        {
            //Debug.Log("FOUND");
            ec.allEnemies = new List<GameObject>();
            ec.allEnemies.Add(collision.GetComponentInParent<ElectricBullet>().gameObject);
            isInRange = true;
            isntConnected = true;
            //ec.allEnemies.Add(FindObjectOfType<EnemyController>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            //Debug.Log("LEFT");
            isInRange = false;
            justLeft = true;
        }
    }
}
