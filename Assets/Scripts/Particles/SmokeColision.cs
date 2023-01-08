using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeColision : MonoBehaviour
{
    [SerializeField]
    string targetTag;
    ParticleSystem ps;
    public List<ParticleCollisionEvent> collisionEvents;    

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(targetTag) || other.CompareTag("Player"))
        {
            int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

            Rigidbody rb = other.GetComponent<Rigidbody>();
            int i = 0;

            while (i < numCollisionEvents)
            {
                if (rb)
                {
                    Vector3 pos = collisionEvents[i].intersection;
                    Vector3 force = collisionEvents[i].velocity * -10;
                    rb.AddForce(force);
                }
                i++;
            }
        }
        
    }
}
