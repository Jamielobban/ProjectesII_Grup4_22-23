using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuerdaGancho : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            if(this.transform.parent.GetComponent<Gancho>().punta != null)
            {
                this.gameObject.GetComponent<LineRenderer>().SetPosition(0,this.transform.parent.position);
                this.gameObject.GetComponent<LineRenderer>().SetPosition(1, this.transform.parent.GetComponent<Gancho>().punta.transform.position);
            }


        }

    }
}
