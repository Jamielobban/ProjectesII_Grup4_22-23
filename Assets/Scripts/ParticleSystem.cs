using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem : MonoBehaviour
{
    private ParticleSystem ps;
    [SerializeField]
    private float duration;
    private float startTime;
    void Start()
    {
        
        startTime = Time.time;
        
    }
    private void Update()
    {
        if (Time.time - startTime >= duration)
        {
            Destroy(this.gameObject);
        }
    }
}
