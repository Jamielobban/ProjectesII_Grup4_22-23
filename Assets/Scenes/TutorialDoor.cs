using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDoor : MonoBehaviour
{
    public bool checkpoint, weapon;
    int save;
    public GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        checkpoint = false;
        weapon = false;

        save = PlayerPrefs.GetInt("doorTutorial", 0);
        door.GetComponent<BoxCollider2D>().enabled = false;

        if (save == 1)
        {
            this.GetComponent<Animator>().SetTrigger("Open");
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            this.GetComponent<Animator>().SetTrigger("Close");
            this.GetComponent<BoxCollider2D>().enabled = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(checkpoint&& weapon&& save == 0)
        {
            this.GetComponent<Animator>().SetTrigger("Open");
            this.GetComponent<BoxCollider2D>().enabled = false;
            PlayerPrefs.SetInt("doorTutorial", 1);

        }

        if(checkpoint && save == 0 && !weapon)
        {
            door.GetComponent<Animator>().SetTrigger("Close");
            door.GetComponent<BoxCollider2D>().enabled = true;
        }
        if (!checkpoint && save == 0 && weapon)
        {
            door.GetComponent<Animator>().SetTrigger("Close");
            door.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
