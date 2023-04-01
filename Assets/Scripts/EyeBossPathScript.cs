using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBossPathScript : MonoBehaviour
{
    CircleCollider2D circleCollider2;
    public LayerMask bossPathMask;
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
        Vector2 raycastOrigin = finalBoss.transform.position + finalBoss.vectorToPlayer.normalized * 1000;

        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, -finalBoss.vectorToPlayer.normalized, 1000, bossPathMask);

        // Comprueba si el Raycast golpea el objeto de destino
        if (hit.collider != null)
        {
            return hit.point;
        }

        return finalBoss.transform.position;

    }



    

   
}
