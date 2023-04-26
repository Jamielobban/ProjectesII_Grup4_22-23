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
    CircleTransition restAnimation;
    BlitController blit;
    Transform playerTtransform;
    CheckpointsList list;

    bool descansar;
    public bool UnlockAtStart;
    string nameSave;
    int? restAudioKey;
    AudioClip restAudio;

    public bool bosque;
    // Start is called before the first frame update
    void Start()
    {
        playerTtransform = GameObject.FindGameObjectWithTag("Player").transform;
        restAudio = Resources.Load<AudioClip>("Sounds/Sewer/RestAudio");
        restAnimation = FindObjectOfType<CircleTransition>();
        blit = FindObjectOfType<BlitController>();
        nameSave = "encendido" + SceneManager.GetActiveScene().buildIndex;
        if (UnlockAtStart&&!encendido)
        {
            PlayerPrefs.SetInt(nameSave, (true ? 1 : 0));
            PlayerPrefs.SetInt("isDead", (true ? 1 : 0));

        }

        if (bosque)
        {
            PlayerPrefs.SetInt(nameSave, (true ? 1 : 0));
            PlayerPrefs.SetInt("IDScene", SceneManager.GetActiveScene().buildIndex);

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

                int i = 0;
                string KeyName = "Sala" + i;
                while (PlayerPrefs.HasKey(KeyName))
                {
                    //Debug.Log(KeyName);
                    PlayerPrefs.SetInt(KeyName, (false ? 1 : 0));
                    i++;
                    KeyName = "Sala" + i;
                }
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
        yield return new WaitForSeconds(1.5f);
        player.canMove = true;
        blit.isResting = false;
        yield return new WaitForSeconds(1.5f);
        descansar = false;
        //Debug.Log("asda");
        //restAnimation.OpenBlackScreen();

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

                //restAnimation.CloseBlackScreen();
                blit.isResting = true;
                restAudioKey = AudioManager.Instance.LoadSound(restAudio, playerTtransform.transform.position, 0, false, transform, MixerGroups.OTHER,1);
                button.SetActive(false);

                PlayerPrefs.SetInt("isDead", (false ? 1 : 0));

                int i = 0;
                string KeyName = "Sala" + i;
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
