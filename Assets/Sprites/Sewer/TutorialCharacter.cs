using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCharacter : MonoBehaviour
{
    bool flip;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        flip = false;
        this.GetComponent<SpriteRenderer>().flipX = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(flip && (player.transform.position.x <= this.transform.position.x))
            {
                flip = false;
                this.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(!flip && (player.transform.position.x > this.transform.position.x))
            {
                flip = true;
                this.GetComponent<SpriteRenderer>().flipX = false;

            }
        }
    }
}
