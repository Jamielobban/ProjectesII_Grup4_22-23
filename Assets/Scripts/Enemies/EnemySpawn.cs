using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawn : MonoBehaviour
{
    public GameObject EnemyPrefab;
    GameObject Enemy;
    private SpriteRenderer spawn;
    GameObject parent;
    public AudioClip enemySpawnSound;
    private void Awake()
    {
        spawn = GetComponent<SpriteRenderer>();
        parent = GameObject.FindGameObjectWithTag("EnemyList");
    }
    public void SpawnEnemy()
    {
        Enemy = Instantiate(EnemyPrefab, transform.position, transform.rotation);
        GameObject.FindGameObjectWithTag("RoomManager").GetComponentInParent<RoomManager>().enemiesInRoom.Add(Enemy);
        Enemy.transform.parent = parent.transform;
        Destroy(this.gameObject);
    }


    public void SpawnAnimation()
    {
        AudioManager.Instance.PlaySound(enemySpawnSound, this.transform.position);
        spawn.DOColor(Color.red, 1f);
        Invoke("SpawnEnemy", 1f);
    }



}