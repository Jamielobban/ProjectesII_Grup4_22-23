using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FlamesColision : MonoBehaviour
{
    ParticleSystem ps;
    public List<ParticleCollisionEvent> collisionEvents;

    List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
    List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

    [SerializeField] string targetTag;
    
    Dictionary<float,GameObject> whoAndWhenIsHit = new Dictionary<float, GameObject>();

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void OnParticleTrigger()
    {
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        // iterate through the particles which entered the trigger and make them red
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            
        }

        // iterate through the particles which exited the trigger and make them green
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            
        }

        // re-assign the modified particles back into the particle system
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(targetTag))
        {
            //Debug.Log("HI");
           


            //int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);
            //Vector3 pos = new Vector3();

            //Rigidbody rb = other.GetComponent<Rigidbody>();
            //int i = 0;

            //while (i < numCollisionEvents)
            //{
            //    if (rb)
            //    {
            //       pos = collisionEvents[i].intersection;                    
            //    }
            //    i++;
            //}

            BulletHitInfo info;
            info.damage = GetComponentInParent<FlameThrowerBullet>().GetFlamesDamage();
            info.impactPosition = other.transform.position;
            other.GetComponent<EnemyController>().GetDamage(info);
            whoAndWhenIsHit.Add(Time.time, other);
        }
        
    }
}
