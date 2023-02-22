using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CheckPoints : MonoBehaviour
{
    public string name;
    public Transform spawn;

    public GameObject velasEncendidas;
    public GameObject velasApagadas;

    bool encendido;

    public GameObject button;

    public GameObject menu;
    public GameObject menuButton;

    public TextMeshProUGUI boton;
    public TextMeshProUGUI nombre;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        menuButton.transform.GetChild(1).GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(Spawn);

        button.SetActive(false);
        menu.SetActive(false);
        velasApagadas.SetActive(true);
        velasEncendidas.SetActive(false);
        menuButton.SetActive(false);

        boton.text = name;
        nombre.text = name;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            button.SetActive(true);

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetButton("Interact"))
            {
                button.SetActive(false);

                menu.SetActive(true);

                menu.GetComponent<MenuCheckPoints>().EnterMenu();


                if (!encendido)
                {
                    encendido = true;
                    velasApagadas.SetActive(false);
                    velasEncendidas.SetActive(true);
                    menuButton.SetActive(true);

                }

                player.canMove = false;
                player.actualSpawn = spawn;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            button.SetActive(false);

        }
    }

    void Spawn()
    {
        player.actualSpawn = spawn;
        player.Spawn();
        player.canMove = true;
        menu.SetActive(false);


    }
}
