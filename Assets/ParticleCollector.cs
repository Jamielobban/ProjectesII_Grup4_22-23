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

    ParticleSystem.TriggerModule triggersBox;
    public BoxCollider2D boxToCollide;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        boxToCollide = GameObject.FindGameObjectWithTag("ParticleCollector").GetComponent<BoxCollider2D>();
        forceField = GameObject.FindGameObjectWithTag("ParticleForce").GetComponent<ParticleSystemForceField>();
        myRb = GetComponent<CircleCollider2D>();
        potions = FindObjectOfType<PotionSystem>();
        limitVelocityOverLifetime = ps.limitVelocityOverLifetime;
        triggersBox = ps.trigger;
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
            //Debug.Log("I ate one");
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
        yield return new WaitForSeconds(0.05f);
        potions.CheckPotionStatus();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Hitting the player");
            ps.externalForces.AddInfluence(forceField);
            triggersBox.AddCollider(boxToCollide);
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
