using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletParticleController : MonoBehaviour
{
    public bool hasCollided = false;
    // Start is called before the first frame update

    private ParticleSystem ps;
    public List <ParticleCollisionEvent> collisionEvents;
    public ParticleSystem.RotationOverLifetimeModule myRotation;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        StartCoroutine(waitForFreeze());
    }
    private IEnumerator waitForFreeze()
    {
        yield return new WaitForSeconds(0.5f);
        ps.Pause();
    }
}
