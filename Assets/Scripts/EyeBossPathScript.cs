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

    public Vector2 GetNextPoint(Enemy13 finalBoss)
    {       

        float rpow2 = Mathf.Pow(circleCollider2.radius,2);
        float bx = finalBoss.transform.position.x;// valor de bx
        float by = finalBoss.transform.position.y;
        float vpx = finalBoss.vectorToPlayer.normalized.x;// valor de vpx
        float vpy = finalBoss.vectorToPlayer.normalized.y; // valor de vpy
        float a = vpx * vpx + vpy * vpy;
        float b = 2 * (bx * vpx + by * vpy);
        float c = bx * bx + by * by - rpow2;
        float discriminante = b * b - 4 * a * c;
        float Z;

        if (discriminante < 0)
        {
            return Vector2.zero;
        }
        else if (discriminante == 0)
        {
            // Solo hay una solución real
            Z = -b / (2 * a);
            // Usar Z para encontrar Xxy
        }
        else
        {  
            Z = (-b + Mathf.Sqrt(discriminante)) / (2 * a);            
            // Usar Z para encontrar Xxy
        }

        return finalBoss.transform.position + finalBoss.vectorToPlayer.normalized * Z;
    }



    

   
}
