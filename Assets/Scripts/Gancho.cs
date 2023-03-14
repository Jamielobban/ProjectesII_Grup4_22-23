using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gancho : MonoBehaviour
{
    public List<GameObject> ganchos;
    // Start is called before the first frame update

    GameObject ganchoMasCercano;
    GameObject player;

    bool enganchado;
    public float fuerza;
    public float fuerzaAbajo;

    public float velocidadCuerda;

    bool addForceDown;

    public GameObject cuerda;

    public GameObject puntaGancho;
    public GameObject punta;

    bool lanzarCuerda;
    void Start()
    {
        lanzarCuerda = false;
        cuerda.SetActive(false);
        punta = null;
        addForceDown = false;
        enganchado = false;
        ganchoMasCercano = null;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (lanzarCuerda)
        {
            punta.transform.position = Vector3.MoveTowards(punta.transform.position, ganchoMasCercano.transform.position, velocidadCuerda * Time.deltaTime);

        }

        if (ganchoMasCercano != null && Input.GetMouseButtonDown(1) && !enganchado)
        {
            enganchado = true;
            lanzarCuerda = true;
            punta = Instantiate(puntaGancho, this.transform.GetChild(0).transform.position, this.transform.GetChild(0).transform.rotation);
            cuerda.SetActive(true);


            player.GetComponent<PlayerMovement>().canMove = false;
            player.GetComponent<PlayerMovement>().disableDash = true;
            player.GetComponent<PlayerMovement>().isDashing = true;

            StartCoroutine(lanzarse());
        }

        if (addForceDown)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1 * fuerzaAbajo), ForceMode2D.Force);

        }
    }
    private IEnumerator lanzarse()
    {
        yield return new WaitForSeconds(0.25f);
        Vector3 vector = (ganchoMasCercano.transform.position - player.transform.position).normalized;
        player.GetComponent<Rigidbody2D>().velocity = (vector * ((Vector3.Distance(ganchoMasCercano.transform.position, player.transform.position)) * fuerza));

        player.layer = LayerMask.NameToLayer("IgnoreEverything");

        addForceDown = true;

        StartCoroutine(delay());
        StartCoroutine(quitarCuerda());

    }

    private IEnumerator quitarCuerda()
    {
        yield return new WaitForSeconds(0.025f);
        lanzarCuerda = false;
        cuerda.SetActive(false);

        Destroy(punta.gameObject);



    }
    private IEnumerator delay()
    {
        yield return new WaitForSeconds(0.125f);
        player.layer = LayerMask.NameToLayer("Player");
        addForceDown = false;
        ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0f);
        ganchoMasCercano = null;
        enganchado = false;
        player.GetComponent<PlayerMovement>().isDashing = false;
        player.GetComponent<PlayerMovement>().canMove = true;
        player.GetComponent<PlayerMovement>().disableDash = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gancho") && !enganchado && player.GetComponent<PlayerMovement>().canMove)
        {
            bool isInside = false;
            for (int i = 0; i < ganchos.Count; i++)
            {

                if (ganchos[i] == collision.gameObject)
                {
                    isInside = true;
                }
            }
            if (!isInside)
                ganchos.Add(collision.gameObject);

            float distancia = 5000;
            for (int i = 0; i < ganchos.Count; i++)
            {
                if (Vector3.Distance(ganchos[i].transform.position, player.transform.position) < distancia)
                {
                    distancia = Vector3.Distance(ganchos[i].transform.position, player.transform.position);

                    if (ganchoMasCercano != null)
                        ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0f);

                    ganchoMasCercano = ganchos[i];
                    ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0.7f);

                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gancho") && !enganchado && player.GetComponent<PlayerMovement>().canMove)
        {
            for (int i = 0; i < ganchos.Count; i++)
            {

                if (ganchos[i] == collision.gameObject)
                {
                    ganchos.RemoveAt(i);
                }
            }

            float distancia = 5000;
            if (ganchos.Count == 0)
            {
                if (ganchoMasCercano != null)
                    ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0f);

                ganchoMasCercano = null;
            }
            else
            {
                for (int i = 0; i < ganchos.Count; i++)
                {
                    float a = Vector3.Distance(ganchos[i].transform.position, player.transform.position);
                    if (Vector3.Distance(ganchos[i].transform.position, player.transform.position) < distancia)
                    {
                        distancia = Vector3.Distance(ganchos[i].transform.position, player.transform.position);

                        if (ganchoMasCercano != null)
                            ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0f);

                        ganchoMasCercano = ganchos[i];
                        ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0.7f);

                    }
                }
            }
        }
    }

}
