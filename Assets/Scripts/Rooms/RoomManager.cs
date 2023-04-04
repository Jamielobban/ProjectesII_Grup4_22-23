using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    //public bool currentRoom = false;
    private PlayerMovement playerAudio;

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
    bool alreadyEnter;
    string nameSave;

    public bool porTriggers;

    public TriggersRound[] triggers;

    int? roomCompleteKey;
    AudioClip roomCompleteAudio;

    int? doorOpenAudioKey;
    int? doorCloseAudioKey;

    AudioClip doorOpenAudio;
    AudioClip doorCloseAudio;
    //Las puertas tienen que estar en el mismo puesto que su palanca
    public bool boss;
    public int bossNumber;
    public void Start()
    {
        if (!boss)
            bossNumber = 0;

        nameSave = "Sala" + SceneManager.GetActiveScene().buildIndex;
        kills = 0;

        playerAudio = FindObjectOfType<PlayerMovement>();
        roomCompleteAudio = Resources.Load<AudioClip>("Sounds/Room/RoomComplete");
        doorOpenAudio = Resources.Load<AudioClip>("Sounds/Door/DoorOpening");
        //Assets / Resources / Sounds / Door / DoorClosing.wav
        doorCloseAudio = Resources.Load<AudioClip>("Sounds/Door/DoorClosing");

        alreadyEnter = (PlayerPrefs.GetInt(nameSave,0) != 0);
        inRoom = false;
        this.gameObject.tag = "Default";
        roomTriggers.SetActive(true);

        for (int i = 0; i < doors.Length; i++)
        {
            if (doors[i].transform.GetChild(0).GetComponent<BoxCollider2D>() != null)
                doors[i].transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
            else
                doors[i].SetActive(false);
        }


        if ((((PlayerPrefs.GetInt("Lado") + 2) % 4 == 3) || ((PlayerPrefs.GetInt("Lado") + 2) % 4 == 0)))
        {
            currentRound = 0;
        }
        else if ((((PlayerPrefs.GetInt("Lado") + 2) % 4 == 2) || ((PlayerPrefs.GetInt("Lado") + 2) % 4 == 1)))
        {
            currentRound = enemiesInEachRound.Length-1;

        }





    }

    void closeDoors()
    {
        doorCloseAudioKey = AudioManager.Instance.LoadSound(doorCloseAudio,playerAudio.transform.position);
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
                if (doors[i].transform.childCount == 1)
                {
                    doors[i].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Close");
                    if (doors[i].transform.GetChild(0).GetComponent<BoxCollider2D>() != null)
                        doors[i].transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = true;
                    else
                        doors[i].SetActive(true);
                }
                else
                {
                    doors[i].SetActive(true);

                }
            }
        }
    }

    void openDoors()
    {
        alreadyEnter = true;
        doorOpenAudioKey = AudioManager.Instance.LoadSound(doorOpenAudio, playerAudio.transform.position);
        PlayerPrefs.SetInt(nameSave, (alreadyEnter ? 1 : 0));

        for (int i = 0; i < doors.Length; i++)
        {
            if (i != 1 || !boss)
            {
                if (doors[i].transform.childCount == 1)
                {
                    doors[i].transform.GetChild(0).GetComponent<Animator>().SetTrigger("Open");
                    if (doors[i].transform.GetChild(0).GetComponent<BoxCollider2D>() != null)
                        doors[i].transform.GetChild(0).GetComponent<BoxCollider2D>().enabled = false;
                    else
                        doors[i].SetActive(false);
                }
                else
                {
                    doors[i].SetActive(false);

                }
            }
        }
    }
    private IEnumerator OpenDoor(float time, int i)
    {
        yield return new WaitForSeconds(time);


    }
    public void StartRound()
    {
        if (!alreadyEnter)
        {
            if(boss)
            {
                if(PlayerPrefs.GetInt("Final"+bossNumber) == 0)
                {
                    roomTriggers.SetActive(false);

                    this.gameObject.tag = "RoomManager";
                    kills = 0;
                    currentRound = 0;
                    inRoom = true;
                    spawnRound(currentRound);
                    closeDoors();
                }

            }
            else
            {
                roomTriggers.SetActive(false);

                this.gameObject.tag = "RoomManager";
                kills = 0;
                currentRound = 0;
                inRoom = true;
                spawnRound(currentRound);
                closeDoors();
            }

        }
    }

    private void FixedUpdate()
    {
        if(triggers.Length != 0 && currentRound != -1 && currentRound !=3)
        {
            
            if (triggers[currentRound].enter)
            {
                if(!inRoom)
                    closeDoors();

                spawnRound(currentRound);
                inRoom = true;

                if ((((PlayerPrefs.GetInt("Lado") + 2) % 4 == 3) || ((PlayerPrefs.GetInt("Lado") + 2) % 4 == 0)))
                {
                    currentRound++;
                }
                else if ((((PlayerPrefs.GetInt("Lado") + 2) % 4 == 2) || ((PlayerPrefs.GetInt("Lado") + 2) % 4 == 1)))
                {
                    currentRound--;
                }
            }
        }
  
    }

    public void Dead()
    {
        kills++;

        if(!porTriggers)
        {
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
        else
        {
            if (kills >= (spawns.Length))
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
        if(spawn.Enemy != null && spawn.Enemy.transform != null && roomEnemies.transform != null)
        {
            spawn.Enemy.transform.SetParent(roomEnemies.transform);
        }
    }
    void endRoom()
    {
        openDoors();
        roomCompleteKey = AudioManager.Instance.LoadSound(roomCompleteAudio, playerAudio.transform.position,0.5f,false,true,2);
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

        for(int i = 0; i < roomEnemies.transform.childCount; i++)
        {
            Destroy(roomEnemies.transform.GetChild(i).gameObject);
        }
 

    }

}