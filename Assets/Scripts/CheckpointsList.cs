using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointsList : MonoBehaviour
{
    public List<CheckPoints> checkpoints;

    public int currentCheckpointId;

    public Transform actualSpawn;

    PlayerMovement player;

    public Transform defaultSpawn;
    public bool find;

    public GameObject allRooms;

    public GameObject destruibles;

    public GameObject spawns;

    // Start is called before the first frame update
    void Start()
    {
        find = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        currentCheckpointId = PlayerPrefs.GetInt("IDCheckpoints",0);
        setTransform(currentCheckpointId);
    }
    public void restart()
    {
        for (int i = 0; i < spawns.transform.childCount; i++)
        {
            //En vez de destruir, recuperarle la vida
            Destroy(spawns.transform.GetChild(i).gameObject.GetComponent<EnemySpawn>().Enemy);

            spawns.transform.GetChild(i).gameObject.GetComponent<EnemySpawn>().SpawnAnimation();

        }
        for (int i = 0; i < allRooms.transform.childCount; i++)
        {
            allRooms.transform.GetChild(i).gameObject.GetComponent<RoomManager>().restartRoom();
        }
        for (int i = 0; i < destruibles.transform.childCount; i++)
        {
            if (destruibles.transform.GetChild(i).gameObject.GetComponent<barrilDestruible>() != null)
                destruibles.transform.GetChild(i).gameObject.GetComponent<barrilDestruible>().restart();
        }
    }
    void setTransform(int id)
    {
        for(int i = 0; i <checkpoints.Count;i++)
        {
            if(checkpoints[i].id == id)
            {
                find = true;
                actualSpawn = checkpoints[i].spawn;
                player.empezar();

            }
        }

        if(find == false)
        {
            actualSpawn = defaultSpawn;
            player.empezar();
        }
    }
    

    public void setId(int id)
    {
        find = true;
        currentCheckpointId = id;
        PlayerPrefs.SetInt("IDCheckpoints", id);
        PlayerPrefs.SetInt("IDScene", SceneManager.GetActiveScene().buildIndex);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
