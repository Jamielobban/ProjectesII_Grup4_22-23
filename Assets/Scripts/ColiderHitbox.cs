using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderHitbox : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    string tagToDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagToDamage))
        {
            if(tagToDamage == "Player")
            {
                collision.GetComponent<PlayerMovement>().TakeDamage(damage);
            }
        }
    }
        
}
