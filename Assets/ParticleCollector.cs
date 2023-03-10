using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
public class ParticleCollector : MonoBehaviour
{
    public ParticleSystem ps;

    public List<ParticleSystem.Particle> particles = new List<ParticleSystem.Particle>();

    [SerializeField] PotionSystem potions;
    int number;

    CircleCollider2D myRb;

    public ParticleSystemForceField forceField;

    ParticleSystem.LimitVelocityOverLifetimeModule limitVelocityOverLifetime;

    private void Start()
    {
        myRb = GetComponent<CircleCollider2D>();
        ps = GetComponent<ParticleSystem>();
        potions = FindObjectOfType<PotionSystem>();
        limitVelocityOverLifetime = ps.limitVelocityOverLifetime;
        //PlayerMask = LayerMask.NameToLayer("Player");
    }

    private void OnParticleTrigger()
    {
        int triggeredParticles = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
        //Debug.Log(ps.particleCount);
        for (int i = 0; i < triggeredParticles; i++)
        {
            ParticleSystem.Particle p = particles[i];
            p.remainingLifetime = 0;
            potions.amountToFill++;
            StartCoroutine(WaitForFlash());
            particles[i] = p;
        }

        if (ps.particleCount == 0)
        {
            Destroy(this.gameObject, 0.5f);
        }
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, particles);
    }

    private IEnumerator WaitForFlash()
    {
        potions.FlashPotion();
        yield return new WaitForSeconds(0.1f);
        potions.CheckPotionStatus();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Hitting the player");
            ps.externalForces.AddInfluence(forceField);
            //ps.velocityOverLifetime.enabled = false;
            limitVelocityOverLifetime.dampen = 0;
            limitVelocityOverLifetime.drag = 0;
            myRb.enabled = false;
        }
    }

    private IEnumerator WaitForStop()
    {
        yield return new WaitForSeconds(0.3f);

    }
}