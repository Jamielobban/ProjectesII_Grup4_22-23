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
    [SerializeField] float timeBurningActive;
    [SerializeField] float timeBetweenBurnings;
    [SerializeField] float burnDamage;

    private float lastBurn = 0;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();

    }
    private void OnParticleTrigger()
    {
        //int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        //// iterate through the particles which entered the trigger and make them red
        //for (int i = 0; i < numEnter; i++)
        //{
        //    ParticleSystem.Particle p = enter[i];
            
        //}

        //// iterate through the particles which exited the trigger and make them green
        //for (int i = 0; i < numExit; i++)
        //{
        //    ParticleSystem.Particle p = exit[i];
            
        //}

        //// re-assign the modified particles back into the particle system
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        //ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(targetTag))
        {
            //Debug.Log(other.GetComponent<EnemyController>().GetActualHealthState());
            
            other.GetComponent<EnemyController>().GetDamage(() =>
            {
                
                
                //if (other.GetComponent<EnemyController>().enemyHealth <= GetComponentInParent<FlameThrowerBullet>().GetFlamesDamage())
                //{
                //    other.GetComponent<EnemyController>().enemyHealth = 0;
                //    other.GetComponent<EnemyController>().isDeath = true;
                //}
                //else
                //{
                //    other.GetComponent<EnemyController>().enemyHealth -= GetComponentInParent<FlameThrowerBullet>().GetFlamesDamage();
                //}





                
                Debug.Log(other.GetComponent<EnemyController>().GetActualHealthState() != HealthStateTypes.BURNED);

                if (other.GetComponent<EnemyController>().GetActualHealthState() != HealthStateTypes.BURNED )
                {                    
                    other.GetComponent<EnemyController>().ApplyNewHealthStateConsequences(timeBurningActive, timeBetweenBurnings, burnDamage, HealthStateTypes.BURNED);                    
                }
                else
                {
                    other.GetComponent<EnemyController>().timeAddedToHealthState += other.GetComponent<EnemyController>().AddTimeToHealthState();
                }
                       

            }
            );
            
        }
        
    }
}
