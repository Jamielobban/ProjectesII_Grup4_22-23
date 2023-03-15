using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRepeating : MonoBehaviour
{
    [SerializeField]
    float timeBetweenDamage;
    [SerializeField]
    int damagePerTic;
    [SerializeField]
    string tagToAffect;

    float lastTimeDamaged = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag(tagToAffect) && tagToAffect == "Player" && Time.time - lastTimeDamaged >= timeBetweenDamage)
        {
            collision.GetComponent<PlayerMovement>().GetDamage(damagePerTic);
            lastTimeDamaged = Time.time;
        }
    }

    private void Update()
    {
        if(GetComponentInParent<SpriteRenderer>().flipX && this.transform.localScale.x > 0)
        {
            Vector3 aux = this.transform.localScale;
            aux.x *= -1;
            this.transform.localScale = aux;
        }
        else if (!GetComponentInParent<SpriteRenderer>().flipX && this.transform.localScale.x < 0)
        {
            Vector3 aux = this.transform.localScale;
            aux.x *= -1;
            this.transform.localScale = aux;
        }
    }
}
