using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricConnection : MonoBehaviour
{
    public List<GameObject> allEnemies;

    public  List<LineController> allLines;

    [SerializeField]
    private LineController linePrefab;

    [SerializeField]
    public Transform origin;

    public bool weaponIsOn;

    LineController newLine;

    [SerializeField]
    private ElectricDistanceCheck distanceLmao;

    [SerializeField]
    private LineController line;

    public bool isConnected = false;
    private void Awake()
    {
        isConnected = false;
        //allEnemies.AddRange(FindObjectsOfType<EnemyController>());
        allLines = new List<LineController>();

        newLine = Instantiate(linePrefab);
        allLines.Add(newLine);
    }
    private void Update()
    {
        if (allEnemies.Count > 0 && allEnemies[0] == null)
        {
            allEnemies.RemoveAt(0);
        }
        
        if (allEnemies == null || allEnemies.Count == 0 || !isConnected)
        {
            return;
        }
        else
        {
            newLine.AssignTarget(origin.position, allEnemies[0].transform);

        }
    }
}
