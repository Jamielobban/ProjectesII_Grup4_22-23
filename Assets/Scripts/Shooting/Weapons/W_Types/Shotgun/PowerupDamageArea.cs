using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerupDamageArea : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer sr;
    [SerializeField]
    float timeActive;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        sr.DOFade(0f, 0.7f);
    }

    private void Update()
    {
        //if(Time.time - startTime >= timeActive)
        //{
        //    Destroy(this.gameObject);
        //}
    }

}
