using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    [SerializeField]
    float timeActive;
    float startTimeVortex;
    // Start is called before the first frame update
    void Start()
    {
        startTimeVortex = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTimeVortex >= timeActive)
        {
            Destroy(this.gameObject);
        }
    }
}
