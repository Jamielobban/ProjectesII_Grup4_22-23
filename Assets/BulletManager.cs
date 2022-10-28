using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletToDestroy;
    public GameObject collisionEffect;

    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Wall":
                Impact();
                break;
            case "Enemy":
                Impact();
                break;
            case "Player":
                Impact();
                break;
            default:
                break;
        }
        //Destroy(bulletToDestroy);
    }
    void Impact()
    {
        Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(bulletToDestroy);
      
    }
}
