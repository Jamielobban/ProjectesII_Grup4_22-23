using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGusanos : EnemySpawn
{
    public List<Vector3> holesPositions;

    // Start is called before the first frame update
    private void Start()
    {

        spawn = this.GetComponent<Animator>();

        if (spawnEnemyAtStart)
        {
            SpawnAnimation();
        }
    }

    public override void SpawnEnemy()
    {
        Enemy = Instantiate(EnemyPrefab, transform.position, transform.rotation);
        Enemy.GetComponent<Enemy3>().holesPositions = holesPositions;

        if (!spawnEnemyAtStart)
            Enemy.transform.parent = parent.transform;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
