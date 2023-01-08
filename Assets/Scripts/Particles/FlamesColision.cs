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
    bool damageable;
    float lastImeDamaged;

    private float lastBurn = 0;

    void OnEnable()
    {
        ps = GetComponent<ParticleSystem>();
        damageable = true;
        lastImeDamaged = 0;

    }
    private void Update()
    {
        if(Time.time - lastImeDamaged >= 0.2f)
        {
            damageable = true;
        }
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
        if (other.CompareTag(targetTag) && damageable)
        {           
            other.gameObject.GetComponent<Entity>().GetDamage(this.GetComponentInParent<Bullet>().GetBUlletDamage(), HealthStateTypes.BURNED, 0, other.transform.position, TransformMovementType.JUMP);
            lastImeDamaged = Time.time;
            damageable = false;
        }
        
    }
}
//other.GetComponent<EnemyController>().GetDamage(() =>
//{

//    GameObject blood = GameObject.Instantiate((GameObject)Resources.Load("Prefab/DamageArea"), other.transform.position, other.transform.rotation);
//    blood.GetComponent<Transform>().localScale = other.transform.localScale * 2;

//    if (other.GetComponent<EnemyController>().enemyHealth <= this.GetComponentInParent<Bullet>().GetBUlletDamage())
//    {
//        other.GetComponent<EnemyController>().enemyHealth = 0;
//        other.GetComponent<EnemyController>().isDeath = true;
//    }
//    else
//    {
//        other.GetComponent<EnemyController>().enemyHealth -= this.GetComponentInParent<Bullet>().GetBUlletDamage();
//    }
//    Debug.Log(other.GetComponent<EnemyController>().actualHealthState);
//    if (other.GetComponent<EnemyController>().GetActualHealthState() != HealthStateTypes.BURNED)
//    {
//        other.GetComponent<EnemyController>().StartCoroutine(other.GetComponent<EnemyController>().ApplyNewHealthStateConsequences(timeBurningActive, timeBetweenBurnings, burnDamage, HealthStateTypes.BURNED));
//    }
//    else
//    {
//        Debug.Log("In");
//        other.GetComponent<EnemyController>().timeAddedToHealthState += other.GetComponent<EnemyController>().AddTimeToHealthState();
//    }


//}
//            );