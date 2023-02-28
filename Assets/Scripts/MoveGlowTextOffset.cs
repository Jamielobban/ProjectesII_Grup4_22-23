using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveGlowTextOffset : MonoBehaviour
{
    float lerpDuration = 1;
    float startValueX;
    float startValueY;
    float endValueX;
    float endValueY;
    float valueToLerpX;
    float valueToLerpY;
    float timeToChangeVelocity;
    float lastTime;
    Material mat;
    [SerializeField]
    float vel = 1;
    void Start()
    {
        
        timeToChangeVelocity = Random.Range(5f, 10f);
        startValueX = Random.Range(-0.1f, 0.1f);
        startValueY = Random.Range(-0.1f, 0.1f);
        lastTime = Time.time;
        mat = GetComponent<SpriteRenderer>().material;
    }
    private void Update()
    {
        //if(Time.time - lastTime >= timeToChangeVelocity)
        //{
        //    StartCoroutine(Lerp());
        //    mat.SetTextureOffset("_OverlayTex", new Vector2(valueToLerpX, valueToLerpY) * Time.time);
        //}
        //else
        //{
        //    mat.SetTextureOffset("_OverlayTex", new Vector2(startValueX, startValueY) * Time.time);
        //}
        mat.SetTextureOffset("_OverlayTex", new Vector2(Mathf.Sin(Time.time), -Mathf.Cos(Time.time)) * vel);

        mat.SetFloat("_RotateUvAmount", (Time.time * 0.5f) % 6.28f);
    }
    IEnumerator Lerp()
    {
        
        float timeElapsed = 0;
        endValueX = Random.Range(-0.1f, 0.1f);
        endValueY = Random.Range(-0.1f, 0.1f);
        while (timeElapsed < lerpDuration)
        {
            if(endValueX > startValueX)
            {
                valueToLerpX = Mathf.Lerp(startValueX, endValueX, timeElapsed / lerpDuration);
            }
            else
            {
                valueToLerpX = Mathf.Lerp(endValueX, startValueX, timeElapsed / lerpDuration);
            }

            if (endValueY > startValueY)
            {
                valueToLerpY = Mathf.Lerp(startValueY, endValueY, timeElapsed / lerpDuration);
            }
            else
            {
                valueToLerpY = Mathf.Lerp(endValueY, startValueY, timeElapsed / lerpDuration);
            }

            timeElapsed += Time.deltaTime;
            
            Debug.Log(valueToLerpX);
            Debug.Log(valueToLerpY);
            yield return null;
        }        
        startValueX = endValueX;
        startValueY = endValueY;
        lastTime = Time.time;
        
    }
}
