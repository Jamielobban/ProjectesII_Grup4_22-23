using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    //public bool currentRoom = false;

    public GameObject roomEnemies;

    public int[] enemiesInEachRound;

    public EnemySpawn[] spawns;

    bool inRoom;

    int currentRound;

    public int kills;

    public GameObject roomTriggers;

    public Palanca[] palancas;

    public GameObject[] doors;

    //public GameObject room;


    //Las puertas tienen que estar en el mismo puesto que su palanca

    public void Start()
    {
        inRoom = false;
        this.gameObject.tag = "Default";
        roomTriggers.SetActive(true);

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(false);
        }
    }

    void closeDoors()
    {
        for(int i = 0; i < doors.Length; i++)
        {
            if(i < palancas.Length)
            {
                if (palancas[i].puertaAbierta == true)
                {
                  
                        doors[i].SetActive(true);
                    if (doors[i].transform.childCount == 1)
                    {
                        doors[i].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Close");

                    }
                }
            }
            else
            {
                doors[i].SetActive(true);
                if (doors[i].transform.childCount == 1)
                {
                    doors[i].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Close");

                }
            }
        }
    }

    void openDoors()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            if(doors[i].transform.childCount == 1)
            {
                doors[i].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Open");
                StartCoroutine(OpenDoor(0.5f, i));
            }
            else
            {
            doors[i].SetActive(false);

            }
        }
    }
    private IEnumerator OpenDoor(float time, int i)
    {
        yield return new WaitForSeconds(time);
        doors[i].SetActive(false);


    }
    public void StartRound()
    {
        roomTriggers.SetActive(false);

        this.gameObject.tag = "RoomManager";
        kills = 0;
        currentRound = 0;
        inRoom = true;
        spawnRound(currentRound);
        closeDoors();
    }
    public void Dead()
    {
        kills++;

        if(kills == enemiesInEachRound[currentRound])
        {
            if(currentRound < (enemiesInEachRound.Length-1))
            {
                currentRound++;
                spawnRound(currentRound);
            }
            else
            {
                endRoom();
            }
        }
    }
    void spawnRound(int round)
    {
        int enemies = 0;
        if(round != 0)
        {
            enemies = enemiesInEachRound[round - 1];
        }

        for(int i = enemies; i < enemiesInEachRound[round]; i++)
        {
            spawns[i].SpawnAnimation();
            StartCoroutine(SetEnemy(1.1f,spawns[i]));

        }
    }

    private IEnumerator SetEnemy(float time, EnemySpawn spawn)
    {
        yield return new WaitForSeconds(time);
        spawn.Enemy.transform.SetParent(roomEnemies.transform);


    }
    void endRoom()
    {
        openDoors();
        this.gameObject.tag = "Default";

        for(int i = 0; i < palancas.Length; i++)
        {
            palancas[i].canOpen = true;
        }
    }

    public void restartRoom()
    {
        openDoors();

        for (int i = 0; i < palancas.Length; i++)
        {
            palancas[i].canOpen = false;
        }

        roomTriggers.SetActive(true);

        this.gameObject.tag = "Default";

        currentRound = 0;
        inRoom = false;

        foreach (GameObject childs in roomEnemies.transform) {
            Destroy(childs);
        }

    }

}