using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesHitPlayer : MonoBehaviour
{
    float lastHitTime = 0;
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player") && (lastHitTime == 0 || Time.time - lastHitTime >= 0.35f)) 
        {
            Debug.Log("OnParticleeeeeeee");
            other.GetComponent<PlayerMovement>().GetDamage(2);
            lastHitTime = Time.time;
        }
    }
    
}
