using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCasing : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rbmine;
    public float rotatespeed;
    public float downspeed;
    void Start()
    {
        rbmine= GetComponent<Rigidbody2D>();
        rbmine.AddForce(new Vector2(0f, 250f));
    }

    // Update is called once per frame
    void Update()
    {
        downspeed += Time.deltaTime * 15;
        rbmine.AddForce(new Vector2(0f, -downspeed));   
    }
}
