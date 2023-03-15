using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireballColaScript : MonoBehaviour
{
    [SerializeField]
    GameObject cola;

    GameObject clone;

    Transform fireball;

    bool firstTimeDone = false;

    List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    private void Start()
    {
        clone = GameObject.Instantiate(cola, this.transform.position, this.transform.rotation, this.transform);
        fireball = GetComponentsInChildren<Transform>().Where(fb => fb.GetComponent<EnemyProjectile>() == true).ToArray()[0];
        particleSystems.Add(clone.GetComponent<ParticleSystem>());
        particleSystems.AddRange(clone.GetComponentsInChildren<ParticleSystem>());
    }

    private void Update()
    {
       
        if(fireball == null && !firstTimeDone)
        {
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            foreach(ParticleSystem ps in particleSystems)
            {
                ps.loop = false;               
            }

            FunctionTimer.Create(() =>
            {
                Destroy(clone.gameObject);
                Destroy(this.gameObject);
            }, 1.2f);

            firstTimeDone = true;

        }
    }
}
