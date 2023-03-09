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


    CheckpointsList list;

    bool descansar;
    public int id;
    public bool UnlockAtStart;
    string nameSave; 

    // Start is called before the first frame update
    void Start()
    {
        nameSave = "encendido" + id;
        encendido = (PlayerPrefs.GetInt(nameSave) != 0);

        if (UnlockAtStart&& !encendido)
        {
            PlayerPrefs.SetInt("IDCheckpoints", id);
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

        list = GameObject.FindGameObjectWithTag("CheckPoints").GetComponent<CheckpointsList>();




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
        if(collision.CompareTag("Player"))
        {
            if(!descansar)
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

                list.restart();


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
                SetSpawn();

                player.SpawnSalaPrincipal();
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

    void SetSpawn()
    {

        list.actualSpawn = spawn;
        list.setId(id);

        player.reiniciar();
    }
}
