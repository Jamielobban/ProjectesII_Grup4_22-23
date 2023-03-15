using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawning : MonoBehaviour
{
    public List<Trampas> trampas;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < trampas.Count; i++)
            {
                trampas[i].StartSpawning();
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < trampas.Count; i++)
            {
                trampas[i].stopSpawning = true;

            }
        }
    }
}
