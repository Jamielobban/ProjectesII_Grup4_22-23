using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckPoints : MonoBehaviour
{
    public Transform spawn;

    public GameObject velasEncendidas;
    public GameObject velasApagadas;

    bool encendido;

    public GameObject button;


    PlayerMovement player;

    Transform playerTtransform;
    CheckpointsList list;

    bool descansar;
    public bool UnlockAtStart;
    string nameSave;

    // Start is called before the first frame update
    void Start()
    {
        playerTtransform = GameObject.FindGameObjectWithTag("Player").transform;
        nameSave = "encendido" + SceneManager.GetActiveScene().buildIndex;
        if (UnlockAtStart)
        {
            PlayerPrefs.SetInt(nameSave, (true ? 1 : 0));
        }
        encendido = (PlayerPrefs.GetInt(nameSave) != 0);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();


        if (encendido)
        {
            if ((PlayerPrefs.GetInt("isDead") != 0))
            {
                GameObject.FindGameObjectWithTag("WeaponGenerator").GetComponent<WeaponGenerator>().restartStates();

                PlayerPrefs.SetInt("Hearts", player.maxHearts);
                player.currentHearts = player.maxHearts;

                playerTtransform.position = spawn.position;
                StartCoroutine(setIsDeadFalse());
            }

            string KeyName = "Sala" + 1;
            int i = 1;
            while (PlayerPrefs.HasKey(KeyName))
            {

                PlayerPrefs.SetInt(KeyName, (false ? 1 : 0));
                i++;
                KeyName = "Sala" + i;
            }
            descansar = false;
            velasApagadas.SetActive(false);
            velasEncendidas.SetActive(true);
        }
        else
        {

            descansar = false;
            velasApagadas.SetActive(true);
            velasEncendidas.SetActive(false);
        }

        button.SetActive(false);





    }
    private IEnumerator setIsDeadFalse()
    {
        yield return new WaitForSeconds(0.5f);

        PlayerPrefs.SetInt("isDead", (false ? 1 : 0));
    }
    void save()
    {
        GameObject.FindGameObjectWithTag("WeaponGenerator").GetComponent<WeaponGenerator>().saveWeaponsState();

    }
    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator move()
    {
        yield return new WaitForSeconds(0.5f);
        player.canMove = true;
        descansar = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!descansar)
                button.SetActive(true);

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetButton("Interact") && !descansar)
            {
                save();
                descansar = true;

                button.SetActive(false);

                PlayerPrefs.SetInt("isDead", (false ? 1 : 0));


                string KeyName = "Sala" + 1;
                int i = 1;
                while (PlayerPrefs.HasKey(KeyName))
                {

                    PlayerPrefs.SetInt(KeyName, (false ? 1 : 0));
                    i++;
                    KeyName = "Sala" + i;
                }

                SetSpawn();

                StartCoroutine(move());
                player.canMove = false;

                if (!encendido)
                {
                    encendido = true;
                    PlayerPrefs.SetInt(nameSave, (encendido ? 1 : 0));
                    velasApagadas.SetActive(false);
                    velasEncendidas.SetActive(true);

                }
            }
            //else if (Input.GetButton("TeleportToBase") && !descansar)
            //{
            //    if (!encendido)
            //    {

            //        encendido = true;
            //        PlayerPrefs.SetInt(nameSave, (encendido ? 1 : 0));
            //        velasApagadas.SetActive(false);
            //        velasEncendidas.SetActive(true);

            //    }
            //    returnBase();
            //}

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            descansar = false;

            button.SetActive(false);

        }
    }
    void returnBase()
    {

        PlayerPrefs.SetInt("IDScene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(2);

        player.SpawnSalaPrincipal();


    }

    void SetSpawn()
    {

        PlayerPrefs.SetInt("IDScene", SceneManager.GetActiveScene().buildIndex);


        player.reiniciar();
    }
}
