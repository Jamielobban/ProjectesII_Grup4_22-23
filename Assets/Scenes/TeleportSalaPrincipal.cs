using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportSalaPrincipal : MonoBehaviour
{
    public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            button.SetActive(true);
            if (Input.GetButton("Interact"))
            {
                SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene"));

            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            button.SetActive(false);

        }
    }

}
