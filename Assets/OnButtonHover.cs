using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OnButtonHover : MonoBehaviour
{
    Material mat;
    static Sequence shineSequence;
    float startTime;
    bool done;
    // Start is called before the first frame update

    public void OnMouseOver()
    {
        Debug.Log("Mouse over");
        mat = GetComponent<Image>().material;
        shineSequence = DOTween.Sequence();
        startTime = Time.time;
        done = false;
        shineSequence.Join(mat.DOFloat(1, "_ShineLocation", 0.5f)).SetDelay(Random.Range(0.05f, 0.1f)).Append(mat.DOFloat(0, "_ShineLocation", 0.5f)).SetDelay(Random.Range(0.1f, 0.25f))/*.SetLoops(-1)*/;
    }

    public void OnMouseExit()
    {
        shineSequence.Kill();
    }
}

