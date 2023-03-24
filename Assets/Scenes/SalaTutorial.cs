using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalaTutorial : MonoBehaviour
{
    public GameObject room, doors1, doors2, blood, npc;
    int save;


    // Start is called before the first frame update
    void Start()
    {
        save = PlayerPrefs.GetInt("doorTutorial", 0);

        if(save == 0)
        {
            room.SetActive(false);
            doors1.SetActive(true);
            doors2.SetActive(false);
            blood.SetActive(true);
            npc.SetActive(true);
        }
        else
        {
            room.SetActive(true);
            doors1.SetActive(false);
            doors2.SetActive(true);
            blood.SetActive(false);
            npc.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
