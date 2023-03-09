using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickWeapons : MonoBehaviour
{

    public GameObject weapons;
    public GameObject button;

    public GameObject chess;

    public Mensajes conversacion;

    bool end;

    public AmmoUISystem ammo;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        weapons.SetActive(false);
        end = false;
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (conversacion.mesajesEnd[0] == true && end == false)
        button.SetActive(true);


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (conversacion.mesajesEnd[0] == true && end == false)
        {
            button.SetActive(true);

            if (Input.GetButton("Interact"))
            {
                weapons.SetActive(true);
                player.disableWeapons = false;
                ammo.DrawAmmo();
                end = true;
                conversacion.conversation++;
                button.SetActive(false);
                chess.GetComponentInChildren<Animator>().SetTrigger("Open");
            }

        }

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (conversacion.mesajesEnd[0] == true && end == false)
            button.SetActive(false);
    }
}
