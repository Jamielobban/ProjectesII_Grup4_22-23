using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGusanos : EnemySpawn
{
    public List<Vector3> holesPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        if (spawnEnemyAtStart)
        {
            SpawnAnimation();
        }
        EnemyPrefab.GetComponent<Enemy3>().holesPositions = holesPositions;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
