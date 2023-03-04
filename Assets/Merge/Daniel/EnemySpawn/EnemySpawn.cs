using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawn : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public GameObject Enemy;
    public GameObject parent;
    public AudioClip enemySpawnSound;
    int? spawnSoundKey;

    private Animator spawn;

    public bool spawnEnemyAtStart;

    private void Start()
    {
        spawn = this.GetComponent<Animator>();
        if (spawnEnemyAtStart)
        {
            SpawnAnimation();
        }
    }
    private void Awake()
    {

        if (!spawnEnemyAtStart)
            parent = GameObject.FindGameObjectWithTag("EnemyList");
    }

    public void SpawnEnemy()
    {
        Enemy = Instantiate(EnemyPrefab, transform.position, transform.rotation);
        if (!spawnEnemyAtStart)
            Enemy.transform.parent = parent.transform;


    }


    public void SpawnAnimation()
    {
        spawn.SetTrigger("Spawn");

        spawnSoundKey = AudioManager.Instance.LoadSound(enemySpawnSound, this.transform.position);
        Invoke("SpawnEnemy", 0.4f);
    }



}