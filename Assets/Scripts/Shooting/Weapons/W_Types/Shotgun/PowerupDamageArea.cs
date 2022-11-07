using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerupDamageArea : MonoBehaviour
{
    [SerializeField]
    GameObject parent;
    [SerializeField]
    SpriteRenderer sr;
    [SerializeField]
    float timeActive;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        timeActive = 0.7f;
        startTime = Time.time;
        sr.DOFade(0f, timeActive);
    }

    private void Update()
    {
        if (Time.time - startTime >= timeActive)
        {
            Destroy(parent);
        }
    }

}
