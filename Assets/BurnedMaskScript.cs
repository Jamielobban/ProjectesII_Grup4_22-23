using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AllIn1SpriteShader;


public class BurnedMaskScript : MonoBehaviour
{
    Material mat;
    enum MaskFadeProcess { MAX, APPEARING, DISAPPEARING}
    MaskFadeProcess mFP;
    float minimum;
    float maximum;
    float t = 0;
    private void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        mFP = MaskFadeProcess.APPEARING;

    }

    private void Update()
    {
        switch (mFP)
        {
            case MaskFadeProcess.MAX:
                t = 0;
                break;
            case MaskFadeProcess.APPEARING:
                minimum = 1;
                maximum = 0;

                mat.SetFloat("_FadeAmount", Mathf.Lerp(minimum, maximum, t));

                if(t >= 1)
                {
                    mFP = MaskFadeProcess.MAX;
                    break;
                }

                t += 5 * Time.deltaTime;

                break;
            case MaskFadeProcess.DISAPPEARING:

                minimum = 0;
                maximum = 1;

                mat.SetFloat("_FadeAmount", Mathf.Lerp(minimum, maximum, t));

                if (t >= 1)
                {
                    Destroy(this.gameObject);
                    break;
                }

                t += 5 * Time.deltaTime;
                break;
            default:
                break;
        }
    }

    public void ChangeToAppear()
    {
        mFP = MaskFadeProcess.APPEARING;
    }
    public void DestroyMe()
    {
        mFP = MaskFadeProcess.DISAPPEARING;
    }
}
//Material material;
//SpriteRenderer sr;
//SpriteRenderer mySr;
//Texture2D text;
//private Vector2 currOffset = Vector2.zero;
//private AllIn1Shader aIOS;
//private SetAtlasUvs sAU;
//bool firstTime = true;
//float distValue = 0;
////[SerializeField]
////bool worm = true;

//void Start()
//{
//    aIOS = GetComponent<AllIn1Shader>();

//    sr = GetComponentsInParent<SpriteRenderer>().Where(sr => (sr.gameObject.GetInstanceID() != this.gameObject.GetInstanceID())).ToArray()[0];
//    mySr = GetComponent<SpriteRenderer>();
//    material = mySr.material;


//}

//// Update is called once per frame
//void Update()
//{
//    //Debug.Log(sr);
//    //Debug.Log(mySr);
//    currOffset.y += -2 * Time.deltaTime;
//    material.SetTextureOffset("_FadeTex", currOffset);
//    //if (worm)
//    //{
//    mySr.sprite = sr.sprite;
//    CreateText();
//    material.SetTexture("_MainTex", text);
//    //material.SetFloat("_FadeTex", -Time.deltaTime * 0.2f);
//    if (firstTime)
//    {
//        aIOS.ToggleSetAtlasUvs(true);
//        sAU = GetComponent<SetAtlasUvs>();
//        sAU.enabled = true;
//        sAU.UpdateEveryFrame(true);
//        firstTime = false;
//    }
//    material.SetFloat("_DistortAmount", distValue);

//    //}

//}

//void CreateText()
//{
//    text = new Texture2D((int)sr.sprite.rect.width, (int)sr.sprite.rect.height);
//    var pixels = sr.sprite.texture.GetPixels((int)sr.sprite.textureRect.x,
//                                             (int)sr.sprite.textureRect.y,
//                                             (int)sr.sprite.textureRect.width,
//                                             (int)sr.sprite.textureRect.height);
//    text.SetPixels(pixels);
//    text.Apply();
//}

//public void SetValues(KeyValuePair<Transform, float> transformAndDistortion)
//{

//    this.transform.localPosition = transformAndDistortion.Key.position;
//    this.transform.rotation = transformAndDistortion.Key.rotation;
//    this.transform.localScale = transformAndDistortion.Key.localScale;

//    distValue = transformAndDistortion.Value;
//}