using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EffectBlood : MonoBehaviour
{
    public float lifetime;
    float currentWaitTime;
    Material material;

    ParticleSystemRenderer ps;
    bool end;
    // Start is called before the first frame update
    void Start()
    {
        end = false;
        ps = this.GetComponent<ParticleSystemRenderer>();
        material = ps.material;

        currentWaitTime = Time.time;
        material.SetFloat("_FadeAmount", 0.47f);
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - currentWaitTime) > lifetime&&!end)
        {
            end = true;
            material.DOFloat(1, "_FadeAmount", 3);
        }
    }
}
