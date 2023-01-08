using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
   
    [SerializeField]
    float velocity;
    float lastEnter;
    float counter;

    

    private void Start()
    {
        lastEnter = Time.time;
        counter = 0;        
    }

    private void FixedUpdate()
    {
        if (Time.time - lastEnter >= 0.01f)
        {
            counter += velocity * 360 * 0.01f;
            transform.rotation = Quaternion.Euler(0, 0, counter);
            lastEnter = Time.time;
            //Debug.Log(counter);
        }
    }
}
