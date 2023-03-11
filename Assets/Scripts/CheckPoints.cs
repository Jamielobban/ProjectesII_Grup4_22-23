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
    public int id;
    public bool UnlockAtStart;
    string nameSave;

    // Start is called before the first frame update
    void Start()
    {
        playerTtransform = GameObject.FindGameObjectWithTag("Player").transform;
        nameSave = "encendido" + id;
        encendido = (PlayerPrefs.GetInt(nameSave) != 0);

        if (UnlockAtStart && !encendido)
        {
            PlayerPrefs.SetInt("IDScene", SceneManager.GetActiveScene().buildIndex);

            encendido = true;
            PlayerPrefs.SetInt(nameSave, (encendido ? 1 : 0));
            descansar = true;
            velasApagadas.SetActive(false);
            velasEncendidas.SetActive(true);
        }
        else
        {

            if (encendido)
            {
                if ((PlayerPrefs.GetInt("isDead") != 0))
                {
                    playerTtransform.position = spawn.position;
                    PlayerPrefs.SetInt("isDead", (false ? 1 : 0));
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

        }
        button.SetActive(false);





        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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
                descansar = true;

                button.SetActive(false);

                PlayerPrefs.SetInt("isDead", (false ? 1 : 0));


                string KeyName = "Sala" + 0;
                int i = 0;
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
            else if (Input.GetButton("TeleportToBase") && !descansar)
            {
                if (!encendido)
                {

                    encendido = true;
                    PlayerPrefs.SetInt(nameSave, (encendido ? 1 : 0));
                    velasApagadas.SetActive(false);
                    velasEncendidas.SetActive(true);

                }
                returnBase();
            }

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
