using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //public bool currentRoom = false;

   
    public int kills = 0;


    public List<GameObject> objectsToEnable;
    bool EnterRoom = false;
    public int enemiesInRoom;

    public GameObject wallToSpawn;


    //public GameObject room;


    public void Start()
    {
        wallToSpawn.SetActive(true);
        foreach (GameObject gameObject in objectsToEnable)
        {
            gameObject.SetActive(false);
        }
        
    }

    private void Update()
    {
        //if(lastNumberEnemiesInRoom != enemiesInRoom.Count)
        //{
        //    int difference = lastNumberEnemiesInRoom - enemiesInRoom.Count;
        //    if(difference > 0)
        //    {
        //        kills += difference;
        //    }
        //    lastNumberEnemiesInRoom = enemiesInRoom.Count;
        //}
        //if (!this.gameObject.CompareTag("RoomManager"))
        //{
        //    kills = 0;
        //}
    }
    public void Dead()
    {
        kills++;

        if(kills == enemiesInRoom)
        {
            Destroy(wallToSpawn);

            Destroy(this.gameObject);
        }
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& !EnterRoom)
        {
            EnterRoom = true;
            foreach (GameObject enemy in objectsToEnable)
            {
                enemy.SetActive(true);
            }
            //currentRoom = true;
            //cameraPos.GetComponent<CameraPos>().x = room;
            wallToSpawn.SetActive(true);
            this.gameObject.tag = "RoomManager";
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