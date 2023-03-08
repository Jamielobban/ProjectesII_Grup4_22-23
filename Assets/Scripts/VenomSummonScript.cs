using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VenomSummonScript : MonoBehaviour
{
    [SerializeField]
    GameObject veg1;
    [SerializeField]
    Vector4 clippingValues1;
    [SerializeField]
    GameObject veg2;
    [SerializeField]
    Vector4 clippingValues2;
    [SerializeField]
    GameObject veg3;
    [SerializeField]
    Vector4 clippingValues3;
    [SerializeField]
    GameObject veg4;
    [SerializeField]
    Vector4 clippingValues4;

    float timePassed;

    Material veg1Mat;
    Material veg2Mat;
    Material veg3Mat;
    Material veg4Mat;

    //X-L
    //Y-R
    //Z-U
    //W-D

    private void Start()
    {
        timePassed = 0;

        veg1Mat = veg1.GetComponent<SpriteRenderer>().material;
        veg2Mat = veg2.GetComponent<SpriteRenderer>().material;
        veg3Mat = veg3.GetComponent<SpriteRenderer>().material;
        veg4Mat = veg4.GetComponent<SpriteRenderer>().material;

        veg1Mat.SetFloat("_RotateUvAmount", 0);
        veg1Mat.SetFloat("_ClipUvLeft", clippingValues1.x);
        veg1Mat.SetFloat("_ClipUvRight", clippingValues1.y);
        veg1Mat.SetFloat("_ClipUvUp", clippingValues1.z);
        veg1Mat.SetFloat("_ClipUvDown", clippingValues1.w);

        veg2Mat.SetFloat("_RotateUvAmount", 0);
        veg2Mat.SetFloat("_ClipUvLeft", clippingValues2.x);
        veg2Mat.SetFloat("_ClipUvRight", clippingValues2.y);
        veg2Mat.SetFloat("_ClipUvUp", clippingValues2.z);
        veg2Mat.SetFloat("_ClipUvDown", clippingValues2.w);

        veg3Mat.SetFloat("_RotateUvAmount", 0);
        veg3Mat.SetFloat("_ClipUvLeft", clippingValues3.x);
        veg3Mat.SetFloat("_ClipUvRight", clippingValues3.y);
        veg3Mat.SetFloat("_ClipUvUp", clippingValues3.z);
        veg3Mat.SetFloat("_ClipUvDown", clippingValues3.w);

        veg4Mat.SetFloat("_RotateUvAmount", 0);
        veg4Mat.SetFloat("_ClipUvLeft", clippingValues4.x);
        veg4Mat.SetFloat("_ClipUvRight", clippingValues4.y);
        veg4Mat.SetFloat("_ClipUvUp", clippingValues4.z);
        veg4Mat.SetFloat("_ClipUvDown", clippingValues4.w);

        veg1.SetActive(true);

        veg1Mat.DOFloat(0, "_ClipUvLeft", 1);
        veg1Mat.DOFloat(0, "_ClipUvRight", 1);
        veg1Mat.DOFloat(0, "_ClipUvUp", 1);
        veg1Mat.DOFloat(0, "_ClipUvDown", 1);

        FunctionTimer.Create(() =>
        {
            veg2.SetActive(true);

            veg2Mat.DOFloat(0, "_ClipUvLeft", 1);
            veg2Mat.DOFloat(0, "_ClipUvRight", 1);
            veg2Mat.DOFloat(0, "_ClipUvUp", 1);
            veg2Mat.DOFloat(0, "_ClipUvDown", 1);

        }, 1);

        FunctionTimer.Create(() =>
        {
            veg3.SetActive(true);

            veg3Mat.DOFloat(0, "_ClipUvLeft", 1);
            veg3Mat.DOFloat(0, "_ClipUvRight", 1);
            veg3Mat.DOFloat(0, "_ClipUvUp", 1);
            veg3Mat.DOFloat(0, "_ClipUvDown", 1);

        }, 2);

        FunctionTimer.Create(() =>
        {
            veg4.SetActive(true);

            veg4Mat.DOFloat(0, "_ClipUvLeft", 1);
            veg4Mat.DOFloat(0, "_ClipUvRight", 1);
            veg4Mat.DOFloat(0, "_ClipUvUp", 1);
            veg4Mat.DOFloat(0, "_ClipUvDown", 1);

        }, 3);

        FunctionTimer.Create(() =>
        {
            this.transform.DOPunchScale(new Vector3(-0.4f, -0.4f, -0.4f), 0.6f, 3);
        }, 4);

    }
    private void Update()
    {
        timePassed += Time.deltaTime;
        veg1Mat.SetFloat("_RotateUvAmount", (timePassed * 2) % 6.28f);
        veg2Mat.SetFloat("_RotateUvAmount", (timePassed * 2) % 6.28f);
        veg3Mat.SetFloat("_RotateUvAmount", (timePassed * 2) % 6.28f);
        veg4Mat.SetFloat("_RotateUvAmount", (timePassed * 2) % 6.28f);
    }
}
