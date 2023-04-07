using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwordsMissileDamage : MonoBehaviour
{
    BoxCollider2D bc;
    [SerializeField]
    SpriteRenderer sr;
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerMovement>().GetDamage(1);
            bc.enabled = false;
            sr.material.DOFade(0, 0.15f);
        }
    }
}
