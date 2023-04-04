using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public TutorialDoor door;

    public bool weapon;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && Input.GetButton("Interact")&& !weapon)
        {
            door.checkpoint = true;
        }
        else if(collision.CompareTag("Player") && weapon)
        {
            door.weapon = true;

        }
    }
}
