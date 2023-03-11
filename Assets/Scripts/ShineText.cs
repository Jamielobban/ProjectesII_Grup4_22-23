using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ShineText : MonoBehaviour
{
    Material mat;
    static Sequence shineSequence;
    float startTime;
    bool done;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Image>().material;
        shineSequence = DOTween.Sequence();
        startTime = Time.time;
        done = false;
        shineSequence.Join(mat.DOFloat(1, "_ShineLocation", 0.5f)).SetDelay(Random.Range(1f, 1.5f)).Append(mat.DOFloat(0, "_ShineLocation", 0.5f)).SetDelay(Random.Range(0.8f, 1.5f))/*.SetLoops(-1)*/;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Time.time - startTime >= 1.5f && !done)
        //{
        //    mat.DOFloat(1, "_ShineLocation", 0.3f);
        //    done = true;
        //}



    }
}
