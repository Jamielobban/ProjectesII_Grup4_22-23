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

    [SerializeField]
    private LineController line;

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
        if (allEnemies == null || allEnemies.Count == 0)
        {
            return;
        }
        else
        {
            newLine.AssignTarget(origin.position, allEnemies[0].transform);

        }
    }
}
