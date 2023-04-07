using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestrouFunc : MonoBehaviour
{
    public bool fadeOnDisappear = false;
    public float fadeTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyFunction()
    {
        if (fadeOnDisappear)
        {
            GetComponent<SpriteRenderer>().material.DOFade(0, fadeTime);
            Destroy(this.gameObject, fadeTime);
        }
        else
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
