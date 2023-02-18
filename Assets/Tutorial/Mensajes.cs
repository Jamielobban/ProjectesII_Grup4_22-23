using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Mensajes : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI text;
    public string[] mesajes;
    public GameObject button;

    public string startMesage;

    bool pressed;
    public bool start;

    public int conversation;
    public int[] mesajesInConversation;

    public bool[] mesajesEnd;

    public float startTime;

    // Start is called before the first frame update
    void Start()
    {
        conversation = 0; 
        start = false;
        pressed = false;
        panel.SetActive(false);
        text.text = startMesage;
        button.SetActive(false);

        if(startTime != 0)
        {
         StartCoroutine(delay(startTime));
        }
     

    }
    public IEnumerator sendFirstMesage(float time)
    {
        yield return new WaitForSeconds(time);

        if (startMesage != "")
            panel.SetActive(true);

        start = true;
    }

    private IEnumerator delay(float time)
    {
        yield return new WaitForSeconds(time);

        if (startMesage != "")
            panel.SetActive(true);

        start = true;
    }

    private IEnumerator mensages(float time, int mensages)
    {
        mesajes[mensages] = mesajes[mensages].Replace("\\n", "\n");

        text.text = mesajes[mensages];

        mensages++;

        yield return new WaitForSeconds(time);

        if(mensages < mesajesInConversation[conversation+1])
            StartCoroutine(this.mensages(mesajes[mensages].Length * 0.1f, mensages));
        else
        {
            pressed = false;
            panel.SetActive(false);
            mesajesEnd[conversation] = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && start == true && pressed == false)
        {
            button.SetActive(true);
            
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && start == true && pressed == false)
        {
            button.SetActive(true);

            if (Input.GetButtonDown("Interact"))
            {
                pressed = true;
                button.SetActive(false);
                panel.SetActive(true);

                StartCoroutine(mensages((mesajes[mesajesInConversation[conversation]].Length)*0.1f, mesajesInConversation[conversation]));
            }
   

        }

    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        button.SetActive(false);

    }

}
