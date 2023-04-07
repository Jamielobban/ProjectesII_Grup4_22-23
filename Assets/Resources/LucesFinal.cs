using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucesFinal : MonoBehaviour
{
    public GameObject[] luz;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < luz.Length; i++)
        {
            luz[i].SetActive(false);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < luz.Length; i++)
            {
                luz[i].SetActive(true);

            }
        }
    }
}
