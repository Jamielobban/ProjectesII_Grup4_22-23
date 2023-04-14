using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimacionFinal : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMov;
    public Transform[] positions;

    bool caminar1;
    bool caminar2;
    bool caminar3;

    public float velocity;
    public Animator final;
    // Start is called before the first frame update
    void Start()
    {
        caminar1 = false;
        caminar2 = false;
        caminar3 = false;

        player = GameObject.FindGameObjectWithTag("Player");
        playerMov = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (caminar1)
        {
            if(Vector3.Distance(positions[0].transform.position, player.transform.position) > 0.01f)
            {
                caminar(0);
            }
            else
            {
                caminar1 = false;

                player.transform.GetChild(1).GetComponent<Animator>().SetTrigger("EnterRoom");
                Invoke("caminarTrono", 1);
            }
        }

        if (caminar2)
        {
            if (Vector3.Distance(positions[1].transform.position, player.transform.position) > 0.01f)
            {
                caminar(1);
            }
            else
            {
                caminar2 = false;

                player.transform.GetChild(1).GetComponent<Animator>().SetTrigger("EnterRoom");
                Invoke("caminarFinal", 2);
            }
        }
        if (caminar3)
        {
            if (Vector3.Distance(positions[2].transform.position, player.transform.position) > 0.01f)
            {
                caminar(2);
            }
            else
            {
                caminar3 = false;

                player.transform.GetChild(1).GetComponent<Animator>().SetTrigger("EnterRoom");
                player.transform.GetChild(1).GetComponent<Animator>().SetBool("isMoving",false);
                Invoke("transicionFinal", 2);
            }
        }
    }
    void caminarTrono()
    {
        caminar2 = true;
        player.transform.GetChild(1).GetComponent<Animator>().SetTrigger("ExitRoom");

    }
    void caminarFinal()
    {
        velocity = velocity / 3;
        caminar2 = false;
        caminar3 = true;
        player.transform.GetChild(1).GetComponent<Animator>().SetTrigger("ExitRoom");
        player.transform.GetChild(1).GetComponent<Animator>().SetBool("isMoving", false);
    }
    void caminar(int pos)
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, positions[pos].transform.position, velocity * Time.deltaTime);

    }

    void transicionFinal()
    {
        final.SetTrigger("Final");
        Invoke("volverMenu", 4);

    }
    void volverMenu()
    {
        SceneManager.LoadScene(0);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerMov.disableDash = true;
            player.GetComponentInChildren<RightHand>().weaponEquiped = false;
            playerMov.canMove = false;
            player.transform.GetChild(1).GetComponent<Animator>().SetTrigger("ExitRoom");
            caminar1 = true;

        }
    }
}
