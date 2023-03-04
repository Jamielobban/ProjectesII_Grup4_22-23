using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayerOnEnter : MonoBehaviour
{
    [SerializeField]
    float damage;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().GetDamage(damage);
        }
    }
}
