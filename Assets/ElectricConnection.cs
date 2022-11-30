using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricConnection : MonoBehaviour
{
    public List<EnemyController> allEnemies;

    private List<LineController> allLines;

    [SerializeField]
    private LineController linePrefab;

    [SerializeField]
    private Transform origin;

    public bool weaponIsOn;

    LineController newLine;

    [SerializeField]
    private ElectricDistanceCheck distanceLmao;
    private void Start()
    {
            allEnemies.Add(FindObjectOfType<EnemyController>());
            allLines = new List<LineController>();
        
            newLine = Instantiate(linePrefab);
            allLines.Add(newLine);
    }
    private void Update()
    {
        if (distanceLmao.isInRange)
        {
            newLine.AssignTarget(origin.position, allEnemies[0].transform);
        }
    }
}
