using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaSangre : MonoBehaviour
{
    GameObject[] a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
       
    }

    private void FixedUpdate()
    {
        a = GameObject.FindGameObjectsWithTag("Blood");

        for(int i = 0; i < a.Length; i++)
        {
            a[i].transform.SetParent(this.transform);
            a[i].tag = "default";
        }
        a = GameObject.FindGameObjectsWithTag("Recogibles");
        for (int i = 0; i < a.Length; i++)
        {
            a[i].transform.SetParent(this.transform);
            a[i].tag = "default";
        }
    }
}
