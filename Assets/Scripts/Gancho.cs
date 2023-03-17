using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gancho : MonoBehaviour
{
    public List<GameObject> ganchos;
    // Start is called before the first frame update

    public GameObject ganchoMasCercano;
    GameObject player;

    public bool enganchado;
    public float fuerza;
    public float fuerzaAbajo;

    public float velocidadCuerda;

    bool addForceDown;
    bool lanzado;

    public GameObject cuerda;

    public GameObject puntaGancho;
    public GameObject punta;
    public GameObject puntaRecoger;

    bool lanzarCuerda;

    public float timePressed;
    public float startTime;

    public bool pressed;

    public bool volverGancho;
    void Start()
    {
        lanzado = false;
        volverGancho = false;
        pressed = false;
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


   
        if(!enganchado && Input.GetMouseButtonDown(1))
        {
            enganchado = true;

            punta = Instantiate(puntaRecoger, this.transform.GetChild(0).transform.position, this.transform.GetChild(0).transform.rotation);
            Vector3 vector = (punta.transform.position - (this.transform.GetChild(0).transform.position+(punta.transform.up*3))).normalized;

            punta.GetComponent<Rigidbody2D>().velocity = (vector * ((Vector3.Distance(punta.transform.position, (punta.transform.position + (punta.transform.up * 3)))) * -20));

            cuerda.SetActive(true);

            StartCoroutine(volver());

        }

        if (addForceDown)
        {
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -1 * fuerzaAbajo), ForceMode2D.Force);

        }

        if(punta != null && punta.GetComponent<PuntaGancho>().enganche != null&&punta.transform.childCount == 1 && !lanzado)
        {
            player.GetComponent<PlayerMovement>().isDashing = true;
            player.GetComponent<PlayerMovement>().canMove = false;
            player.GetComponent<PlayerMovement>().disableDash = true;
            lanzado = true;
            StartCoroutine(lanzarse());
        }



    }
    private IEnumerator volver()
    {
        yield return new WaitForSeconds(0.25f);
        if(lanzado)
        {
            lanzado = false;
            yield break;
        }


        punta.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        Vector3 vector = (punta.transform.position - player.transform.position).normalized;
        punta.GetComponent<Rigidbody2D>().velocity = (vector * ((Vector3.Distance(punta.transform.position, player.transform.position)) * -10));
        StartCoroutine(acabar());
    }

    private IEnumerator acabar()
    {
        yield return new WaitForSeconds(0.075f);

        if(punta.GetComponentInChildren<AmmoScriptColider>() != null)
        {
            punta.GetComponentInChildren<AmmoScriptColider>().recogerAmmo();
        }

        timePressed = -0.75f;
        enganchado = false;
        cuerda.SetActive(false);
        if(punta != null)
            Destroy(punta.gameObject);



    }
    private IEnumerator reiniciarTiempo()
    {
        yield return new WaitForSeconds(0.1f);
        timePressed = 0;

    }
    private IEnumerator lanzarse()
    {
        yield return new WaitForSeconds(0f);
        Vector3 vector = (punta.GetComponent<PuntaGancho>().enganche.transform.position - player.transform.position).normalized;
        player.GetComponent<Rigidbody2D>().velocity = (vector * ((Vector3.Distance(punta.GetComponent<PuntaGancho>().enganche.transform.position, player.transform.position)) * fuerza));

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
        ganchoMasCercano = null;
        enganchado = false;
        player.GetComponent<PlayerMovement>().isDashing = false;
        player.GetComponent<PlayerMovement>().canMove = true;
        player.GetComponent<PlayerMovement>().disableDash = false;


    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Gancho") && !enganchado && player.GetComponent<PlayerMovement>().canMove)
    //    {
    //        bool isInside = false;
    //        for (int i = 0; i < ganchos.Count; i++)
    //        {

    //            if (ganchos[i] == collision.gameObject)
    //            {
    //                isInside = true;
    //            }
    //        }
    //        if (!isInside)
    //            ganchos.Add(collision.gameObject);

    //        float distancia = 5000;
    //        for (int i = 0; i < ganchos.Count; i++)
    //        {
    //            if (Vector3.Distance(ganchos[i].transform.position, player.transform.position) < distancia)
    //            {
    //                distancia = Vector3.Distance(ganchos[i].transform.position, player.transform.position);

    //                if (ganchoMasCercano != null)
    //                    ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0f);

    //                ganchoMasCercano = ganchos[i];
    //                ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0.7f);

    //            }
    //        }
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Gancho") && !enganchado && player.GetComponent<PlayerMovement>().canMove)
    //    {
    //        for (int i = 0; i < ganchos.Count; i++)
    //        {

    //            if (ganchos[i] == collision.gameObject)
    //            {
    //                ganchos.RemoveAt(i);
    //            }
    //        }

    //        float distancia = 5000;
    //        if (ganchos.Count == 0)
    //        {
    //            if (ganchoMasCercano != null)
    //                ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0f);

    //            ganchoMasCercano = null;
    //        }
    //        else
    //        {
    //            for (int i = 0; i < ganchos.Count; i++)
    //            {
    //                float a = Vector3.Distance(ganchos[i].transform.position, player.transform.position);
    //                if (Vector3.Distance(ganchos[i].transform.position, player.transform.position) < distancia)
    //                {
    //                    distancia = Vector3.Distance(ganchos[i].transform.position, player.transform.position);

    //                    if (ganchoMasCercano != null)
    //                        ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0f);

    //                    ganchoMasCercano = ganchos[i];
    //                    ganchoMasCercano.GetComponent<SpriteRenderer>().material.SetFloat("_OutlineAlpha", 0.7f);

    //                }
    //            }
    //        }
    //    }
    //}

}
