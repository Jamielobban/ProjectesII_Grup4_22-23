using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caida : MonoBehaviour
{
    PlayerMovement player;
    SpriteRenderer sr;
    Animator anim;

    
    // Start is called before the first frame update
    void Start()
    {
        anim = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<Animator>();
        sr = GameObject.FindGameObjectWithTag("Player").transform.GetChild(1).GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator reaparecer(GameObject sombra)
    {
        yield return new WaitForSeconds(0.5f);

        player.reaparecerCaida();
        sombra.transform.GetChild(3).gameObject.SetActive(true);


    }
    private IEnumerator daño(GameObject sombra)
    {
        yield return new WaitForSeconds(0.5f);
        player.GetDamage(1);

        if (!player.isDead)
        {
            StartCoroutine(reaparecer(sombra));
        }
        sr.enabled = false;
        if (anim.GetBool("Fall"))
        {
            anim.SetBool("Return", true);
            anim.SetBool("Fall", false);
        }
    }
    private IEnumerator caer()
    {
        yield return new WaitForSeconds(0.1f);

        anim.SetBool("Fall", true);


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && (collision.transform.parent.GetComponent<MovingPlatform>() == null))
        {
            collision.transform.GetChild(3).gameObject.SetActive(false);
            sr.sortingOrder = -53;
            anim.SetBool("Return", false);

            anim.SetBool("Fall", true);

            player.canMove = false;
            player.disableDash = true;
            player.disableWeapons = true;
            StartCoroutine(daño(collision.transform.GetChild(3).gameObject));


        }
    }

}
