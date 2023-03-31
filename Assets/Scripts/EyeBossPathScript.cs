using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossPathScript : MonoBehaviour
{
    CircleCollider2D circleCollider2;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider2 = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetNextPoint()
    {
        Vector2 center = circleCollider2.bounds.center;
        float radius = circleCollider2.radius;
        return center + Random.insideUnitCircle * radius;        
    }



    //Obtenemos el centro del círculo


    //Obtenemos el radio del círculo collider

    // Creamos un vector 2D aleatorio dentro del área del círculo

   
}
