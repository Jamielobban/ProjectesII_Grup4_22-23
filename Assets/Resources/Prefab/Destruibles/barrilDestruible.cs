using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrilDestruible : MonoBehaviour
{
    public GameObject[] modelos;

    public GameObject modelo;

    public GameObject humo;
    public GameObject hearth;

    bool destruido;

    public int layer;
   
    // Start is called before the first frame update
    void Start()
    {
        humo.GetComponent<SpriteRenderer>().sortingOrder = layer+1;

        modelo.SetActive(true);
        destruido = false;

        if (modelo.transform.childCount == 0)
        {
            modelo.GetComponent<SpriteRenderer>().sortingOrder = layer;

        }
        else
        {
            for (int i = 0; i < modelo.transform.childCount; i++)
            {
                if(modelo.transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                {
                    modelo.transform.GetChild(i).GetComponent<SpriteRenderer>().sortingOrder = layer;
                }
            }
        }
        for (int i = 0; i < modelos.Length; i++)
        {
            if(modelos[i].transform.childCount == 0)
            {
            modelos[i].GetComponent<SpriteRenderer>().sortingOrder = layer;

            }
            else
            {
                 for (int e = 0; e < modelos[i].transform.childCount; e++)
                {
                    modelos[i].transform.GetChild(e).GetComponent<SpriteRenderer>().sortingOrder = layer;
                }
            }
       
        }
    }
    void destroyModel()
    {
        int i = Random.RandomRange(0, (modelos.Length-1));
        modelo.SetActive(false);
        modelos[i].SetActive(true);
        humo.GetComponent<Animator>().SetTrigger("Open");
        destruido = true;

        int rand = Random.RandomRange(0, 15);
        if(rand == 0)
            Instantiate(hearth, this.transform.position, Quaternion.identity, this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if((collision.CompareTag("Bullet")|| collision.CompareTag("EnemyBullet")) && !destruido)
        {
            destroyModel();
        }
    }

    public void restart()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).GetComponent<Curacion>() != null)
            {
                Destroy(this.transform.GetChild(i).gameObject);
            }
        }
        for(int i = 0; i < modelos.Length; i++)
        {
        modelos[i].SetActive(false);
        }
        modelo.SetActive(true);

        destruido = false;
    }
}
