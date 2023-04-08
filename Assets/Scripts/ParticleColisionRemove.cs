using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColisionRemove : MonoBehaviour
{
    public float timeToActiveFalse;
    [HideInInspector]
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
       
    }

    void OnParticleCollision(GameObject other)
    {

        if (other.CompareTag("MapLimit"))
        {
            StartCoroutine(Desactivar());
        }
    
    }

    IEnumerator Desactivar()
    {
        // Esperar un segundo
        yield return new WaitForSeconds(timeToActiveFalse);

        // Desactivar el objeto
        part.gameObject.SetActive(false);
    }
}
