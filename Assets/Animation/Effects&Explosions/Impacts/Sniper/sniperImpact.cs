using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniperImpact : MonoBehaviour
{
    [SerializeField]
    float timeActive;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime >= timeActive)
        {
            Destroy(this.gameObject);
        }
    }
}