using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //public bool currentRoom = false;

    private int lastNumberEnemiesInRoom;
   
    public int kills = 0;

    public List<GameObject> enemySpawners;

    public List<GameObject> enemiesInRoom;

    bool EnterRoom = false;
    public int[] enemiesInRound;
    int round = 0;

    public GameObject wallToSpawn;
    public GameObject wallToSpawn2;

    public GameObject room;

    GameObject cameraPos;

    public void Start()
    {
        cameraPos = GameObject.FindGameObjectWithTag("CameraPos");
        wallToSpawn.SetActive(false);
        wallToSpawn2.SetActive(false);
        lastNumberEnemiesInRoom = enemiesInRoom.Count;
        
    }

    private void Update()
    {
        if(lastNumberEnemiesInRoom != enemiesInRoom.Count)
        {
            int difference = lastNumberEnemiesInRoom - enemiesInRoom.Count;
            if(difference > 0)
            {
                kills += difference;
            }
            lastNumberEnemiesInRoom = enemiesInRoom.Count;
        }
        if (!this.gameObject.CompareTag("RoomManager"))
        {
            kills = 0;
        }
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
            this.gameObject.tag = "Default";
            wallToSpawn.SetActive(false);
            wallToSpawn2.SetActive(false);
            Destroy(gameObject);
        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //currentRoom = true;
            cameraPos.GetComponent<CameraPos>().x = room;
            wallToSpawn.SetActive(true);
            wallToSpawn2.SetActive(true);
            this.gameObject.tag = "RoomManager";
            Invoke("spawnRound", 1.1f);
            Destroy(this.GetComponent<BoxCollider2D>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //if (collision.CompareTag("Player"))
        //{
        //    currentRoom = false;
        //    //Destroy(gameObject);
        //}
    }
}