using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    public GameObject newWeapon;
    public GameObject button;

    public GameObject chess;

    public Palanca palanca;


    bool end;

    // Start is called before the first frame update
    void Start()
    {
        palanca.canOpen = false;
        end = false;
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (end == false)
            button.SetActive(true);


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (end == false)
        {
            button.SetActive(true);

            if (Input.GetButton("Interact"))
            {
                palanca.canOpen = true;

                end = true;
                button.SetActive(false);
                chess.GetComponentInChildren<Animator>().SetTrigger("Open");

            }

        }

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (end == false)
            button.SetActive(false);
    }
}
