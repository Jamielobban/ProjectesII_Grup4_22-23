using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricConnection : MonoBehaviour
{
    public List<GameObject> allEnemies;

    private List<LineController> allLines;

    [SerializeField]
    private LineController linePrefab;

    [SerializeField]
    private Transform origin;

    public bool weaponIsOn;

    LineController newLine;

    [SerializeField]
    private ElectricDistanceCheck distanceLmao;

    private bool isntConnected;
    private void Awake()
    {
        isntConnected = false;
        //allEnemies.AddRange(FindObjectsOfType<EnemyController>());
        allLines = new List<LineController>();

        newLine = Instantiate(linePrefab);
        allLines.Add(newLine);
    }
    private void Update()
    {
        //if(allEnemies.Count > 1)
        //{
        //    allEnemies.RemoveAt(1);
        //}
        if (distanceLmao.isInRange)
        {
            //allEnemies.Add(enemy);
            newLine.AssignTarget(origin.position, allEnemies[0].transform);
        }
        if (distanceLmao.justLeft)
        {
            distanceLmao.justLeft = false;
            Destroy(newLine.gameObject);
        }
    }
}
