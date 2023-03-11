using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitRoom : MonoBehaviour
{
    // Start is called before the first frame update
    public bool vertical;
    public int sentido;

    public int siguienteEscena;
    public Animator anim;
    public int lado;
    PlayerMovement player;
    public GameObject playerTransform;
    public Transform spawn;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        if (((PlayerPrefs.GetInt("Lado")+2)%4 == lado) && ((PlayerPrefs.GetInt("isDead") == 0)))
        {
            GameObject.FindGameObjectWithTag("WeaponGenerator").GetComponent<WeaponGenerator>().GetWeapons();
            if (!vertical)
            {
                if (sentido == -1)
                {
                    playerTransform.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = false;
                }
                else
                {
                    playerTransform.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = true;

                }
            }

            playerTransform.transform.position = spawn.position;
            playerTransform.transform.GetChild(1).GetComponent<Animator>().SetTrigger("ExitRoom");

            anim.SetTrigger("TransicionEntrar");
            player.disableDash = true;
            player.GetComponentInChildren<RightHand>().weaponEquiped = false;
            player.canMove = false;


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator cambioEscena()
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(siguienteEscena);
    }
    private IEnumerator transicion()
    {
        yield return new WaitForSeconds(0.5f);

        anim.SetTrigger("Transicion");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (!player.entrandoSala)
            {
                StartCoroutine(transicion());
                StartCoroutine(cambioEscena());


                if (!vertical)
                {
                    if (sentido == 1)
                    {
                        collision.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else
                    {
                        collision.transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = true;

                    }
                }
                PlayerPrefs.SetInt("Lado", lado);

                player.disableDash = true;
                player.GetComponentInChildren<RightHand>().weaponEquiped = false;
                player.canMove = false;

                collision.transform.GetChild(1).GetComponent<Animator>().SetTrigger("ExitRoom");

            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!player.entrandoSala)
            {
                if (vertical)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 200 * sentido, 0), ForceMode2D.Force);
                }
                else
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector3(200 * sentido, 0, 0), ForceMode2D.Force);

                }
            }
            else
            {
                if (vertical)
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 200 * sentido*-1, 0), ForceMode2D.Force);
                }
                else
                {
                    collision.GetComponent<Rigidbody2D>().AddForce(new Vector3(200 * sentido * -1, 0, 0), ForceMode2D.Force);

                }
            }
   
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.entrandoSala = false;
            player.disableDash = false;
            player.GetComponentInChildren<RightHand>().weaponEquiped = true;
            player.canMove = true;
            playerTransform.transform.GetChild(1).GetComponent<Animator>().SetTrigger("EnterRoom");

        }
    }
}
