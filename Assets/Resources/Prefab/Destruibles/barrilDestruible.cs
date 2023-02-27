using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrilDestruible : MonoBehaviour
{
    public GameObject[] modelos;

    public GameObject modelo;

    public GameObject humo;

    bool destruido;

    public int layer;
    // Start is called before the first frame update
    void Start()
    {
        humo.GetComponent<SpriteRenderer>().sortingOrder = layer+1;

        modelo.SetActive(true);
        destruido = false;
        modelo.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = layer;
        modelo.transform.GetChild(1).GetComponent<SpriteRenderer>().sortingOrder = layer;

        for (int i = 0; i <3; i++)
        {
            modelos[i].GetComponent<SpriteRenderer>().sortingOrder = layer;
        }
    }
    void destroyModel()
    {
        int i = Random.RandomRange(0, 2);
        modelo.SetActive(false);
        modelos[i].SetActive(true);
        humo.GetComponent<Animator>().SetTrigger("Open");
        destruido = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet") && !destruido)
        {
            destroyModel();
        }
    }

    public void restart()
    {
        for(int i = 0; i < 3; i++)
        {
        modelos[i].SetActive(false);
        }
        modelo.SetActive(true);

        destruido = false;
    }
}
