using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveGlowTextOffset : MonoBehaviour
{
    Material circleMat;       
    static Sequence textureMovement;   
    float timeBetweenChange;
    float lastTime;
    float scrollSpeedX;
    float scrollSpeedY;
    float scrollSpeedX2;
    float scrollSpeedY2;
    bool doingLerp = false;
    float lerpTime = 1;
    bool firstTime = true;

    float lerpValueX;
    float lerpValueY;

    // Start is called before the first frame update
    void Start()
    {
        circleMat = GetComponent<SpriteRenderer>().material;

        timeBetweenChange = Random.Range(5f, 15f);

        lastTime = Time.time;

        scrollSpeedX = Random.Range(-0.5f, 0.5f);
        scrollSpeedY = Random.Range(-0.5f, 0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if(Time.time - (lastTime + lerpTime) >= timeBetweenChange || firstTime)
        {
            //timeBetweenChange = Random.Range(5f, 15f);
            scrollSpeedX2 = Random.Range(-0.5f, 0.5f);
            scrollSpeedY2 = Random.Range(-0.5f, 0.5f);
            doingLerp = true;
            lastTime = Time.time;
            firstTime = false;
        }

        if (doingLerp)
        {
            lerpValueX = Mathf.Lerp(scrollSpeedX, scrollSpeedX2, lerpTime);
            lerpValueY = Mathf.Lerp(scrollSpeedY, scrollSpeedY2, lerpTime);

            circleMat.SetTextureOffset("_OverlayTex", new Vector2(Time.time * lerpValueX, Time.time * lerpValueY));

            if(lerpValueX >= scrollSpeedX2 && lerpValueY >= scrollSpeedY2)
            {
                doingLerp = false;
                scrollSpeedX = lerpValueX;
                scrollSpeedY = lerpValueY;
            }

        }
        else
        {
            circleMat.SetTextureOffset("_OverlayTex", new Vector2(Time.time * scrollSpeedX, Time.time * scrollSpeedY));
        }
        
       // Debug.Log()
    }
}
