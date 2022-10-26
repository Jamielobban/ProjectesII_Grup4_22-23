using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    [SerializeField]
    private float bulletDamage;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float bulletRange;
    [SerializeField]
    private bool hitSomething;

    [SerializeField]
    private GameObject[] damageableEntities;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
