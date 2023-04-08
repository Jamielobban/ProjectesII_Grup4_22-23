using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Cinematica : MonoBehaviour
{
    // Start is called before the first frame update
    public int llave;
    public CinemachineVirtualCamera camera;
    public Transform cinematica;
    public Transform player;
    public GameObject llave1;
    public GameObject llave2;

    void Start()
    {
        if(PlayerPrefs.GetInt("Final" + llave, 0) == 1)
        {
            llave1.SetActive(false);
            llave2.SetActive(true);

        }
        else
        {
            llave1.SetActive(true);
            llave2.SetActive(false);
        }

        if (PlayerPrefs.GetInt("LLave"+llave, 0) == 1&& PlayerPrefs.GetInt("Final" + llave, 0) == 0)
        {
            Invoke("setFalse", 0.1f);
            camera.Follow = cinematica;
            Invoke("startAnim", 3f);
            Invoke("setTrue", 2f);

            Invoke("volverPlayer", 6f);

        }
    }

    void setFalse()
    {
        PlayerPrefs.SetInt("LLave" + llave, 0);

    }
    void setTrue()
    {
        PlayerPrefs.SetInt("Final" + llave, 1);

    }
    void startAnim()
    {
        llave1.transform.GetChild(0).GetComponent<Animator>().SetTrigger("start");
    }
    void volverPlayer()
    {
        camera.Follow = player;
        llave1.SetActive(false);
        llave2.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
