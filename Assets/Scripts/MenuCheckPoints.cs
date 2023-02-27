using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCheckPoints : MonoBehaviour
{
    public Transform[] positions;

    public GameObject[] mapa1;
    public GameObject[] mapa2;
    public GameObject[] mapa3;

    GameObject currentImage;

    PlayerMovement player;

    public GameObject allRooms;

    public GameObject destruibles;

    public GameObject spawns;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < mapa1.Length; i++)
        {

            GameObject menu = mapa1[i].transform.GetChild(1).gameObject;
            mapa1[i].GetComponent<Button>().onClick.AddListener(() => { changeMenu(menu); });

        }
        for (int i = 0; i < mapa2.Length; i++)
        {
            GameObject menu = mapa2[i].transform.GetChild(1).gameObject;

            mapa2[i].GetComponent<Button>().onClick.AddListener(() => { changeMenu(menu); });
        }
        for (int i = 0; i < mapa3.Length; i++)
        {
            GameObject menu = mapa3[i].transform.GetChild(1).gameObject;

            mapa3[i].GetComponent<Button>().onClick.AddListener(() => { changeMenu(menu); });
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitMenu()
    {
        player.canMove = true;
        this.gameObject.SetActive(false);
    }

    void changeMenu(GameObject image)
    {
        if(currentImage != null)
        {
            currentImage.SetActive(false);
        }
        Debug.Log(image);
        currentImage = image;

        currentImage.SetActive(true);
    }

    public void EnterMenu()
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
            destruibles.transform.GetChild(i).gameObject.GetComponent<barrilDestruible>().restart();
        }

        int e = 0;
        for (int i = 0; i < mapa1.Length; i++)
        {
            mapa1[i].transform.GetChild(1).gameObject.SetActive(false);

            //if (mapa1[i].activeSelf)
            //{
            //    mapa1[i].transform.position = positions[e].position;
            //}
        }

        e = 0;
        for (int i = 0; i < mapa2.Length; i++)
        {
            mapa2[i].transform.GetChild(1).gameObject.SetActive(false);

            //if (mapa2[i].activeSelf)
            //{
            //    mapa2[i].transform.position = positions[e].position;
            //}
        }

        e = 0;
        for (int i = 0; i < mapa3.Length; i++)
        {
            mapa3[i].transform.GetChild(1).gameObject.SetActive(false);

            //if (mapa3[i].activeSelf)
            //{
            //    mapa3[i].transform.position = positions[e].position;
            //}
        }
    }
}
