using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Palanca : MonoBehaviour
{

    public bool puertaAbierta;
    float pressTime;
    bool isPressed;

    public float holdTime;
    public GameObject button;

    public GameObject door;

    public Image slice;

    public bool canOpen;
    // Start is called before the first frame update
    void Start()
    {
        slice.fillAmount = 0;
        button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !puertaAbierta && canOpen)
        {
            button.SetActive(true);
            isPressed = false;


        }
    }

    public virtual void Action()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !puertaAbierta && canOpen)
        {
            if (Input.GetButton("Interact") && !isPressed)
            {
                pressTime = Time.time;
                isPressed = true;

            }

            if (Input.GetKeyUp(KeyCode.E) && isPressed)
            {
                isPressed = false;
                button.SetActive(true);
                slice.fillAmount = 0;

            }


            if (isPressed && ((Time.time - pressTime)>holdTime))
            {
                door.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Open");
                StartCoroutine(open(1f));
                puertaAbierta = true;
                button.SetActive(false);
                this.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().SetTrigger("Open");
                Action();
            }

            if (isPressed)
            {
                slice.fillAmount = (Time.time - pressTime)/ holdTime;

            }
        }
    }

    private IEnumerator open(float time)
    {
        yield return new WaitForSeconds(time);
        door.GetComponent<BoxCollider2D>().enabled = false;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !puertaAbierta && canOpen)
        {
            button.SetActive(false);
            isPressed = false;
            slice.fillAmount = 0;

        }

    }
}
