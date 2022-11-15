using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public List<GameObject> enemySpawners;

    public List<GameObject> enemiesInRoom;

    bool EnterRoom = false;
    public int[] enemiesInRound;
    int round = 0;

    public GameObject wallToSpawn;
    public GameObject wallToSpawn2;
    public void Start()
    {
        wallToSpawn.SetActive(false);
        wallToSpawn2.SetActive(false);
    }
    public void spawnRound()
    {
        //if (enemiesInRound[round] < enemy.Count)
        //{
        //    enemiesInRound[round] = enemy.Count;
        //    EnterRoom = false;
        //}
        if (round < enemiesInRound.Length)
        {
            for (int i = 0; i < enemiesInRound[round]; i++)
            {
                enemySpawners[0].GetComponent<EnemySpawn>().SpawnAnimation();
                enemySpawners.Remove(enemySpawners[0]);
                //GetComponent<EnemySpawn>().SpawnAnimation(spawns);
            }
            round++;
        }
        else
        {
            //Debug.Log("All enemies are dead");
            //this.gameObject.tag = "Default";
            wallToSpawn.SetActive(false);
            wallToSpawn2.SetActive(false);
            Destroy(gameObject);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wallToSpawn.SetActive(true);
            wallToSpawn2.SetActive(true);
            this.gameObject.tag = "RoomManager";
            Invoke("spawnRound", 1.1f);
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }
}