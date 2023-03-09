using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VenomSummonScript : MonoBehaviour
{

    public GameObject bullet;

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

    float startTime;
    float timePassed;
    float lastTimeSetDirection;
    float timeTravelling;
    float timeToStop;

    Material veg1Mat;
    Material veg2Mat;
    Material veg3Mat;
    Material veg4Mat;

    bool stop;
    bool part1Enter = true;
    bool part2Enter = true;
    bool part3Enter = true;
    bool part4Enter = true;

    //X-L
    //Y-R
    //Z-U
    //W-D
    GameObject bulletInstance;

    static Sequence sequenceImpactShader;

    private void Awake()
    {
        veg1.SetActive(false);
        veg2.SetActive(false);
        veg3.SetActive(false);
        veg4.SetActive(false);
    }

    private void Start()
    {
        startTime = Time.time;
        timePassed = 0;
        lastTimeSetDirection = 0;
        stop = false;        

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

            veg4Mat.DOFloat(0, "_ClipUvLeft", 0.8f);
            veg4Mat.DOFloat(0, "_ClipUvRight", 0.8f);
            veg4Mat.DOFloat(0, "_ClipUvUp", 0.8f);
            veg4Mat.DOFloat(0, "_ClipUvDown", 0.8f);

        }, 3);

        

    }
    private void Update()
    {
        
        timePassed += Time.deltaTime;
        veg1Mat.SetFloat("_RotateUvAmount", (timePassed * 2) % 6.28f);
        veg2Mat.SetFloat("_RotateUvAmount", (timePassed * 2) % 6.28f);
        veg3Mat.SetFloat("_RotateUvAmount", (timePassed * 2) % 6.28f);
        veg4Mat.SetFloat("_RotateUvAmount", (timePassed * 2) % 6.28f);        
        

        if((Time.time - lastTimeSetDirection >= timeTravelling || lastTimeSetDirection == 0) && !stop)
        {
            timeTravelling = Random.Range(0.3f, 0.7f);
            Vector3 aux = Random.insideUnitCircle * 3f;
            this.transform.DOLocalMove(aux, timeTravelling);
            lastTimeSetDirection = Time.time;
        }

        if(Time.time-startTime >= 4)
        {
            if (part1Enter)
            {
                stop = true;
                timeToStop = Random.Range(0.3f, 0.81f);
                //timeToStop = 0.3f;
                this.transform.DOLocalMove(Vector3.zero, timeToStop);
                part1Enter = false;
            }
            if(Time.time - startTime >= 4 + timeToStop && part2Enter)
            {
                GameObject empty = new GameObject();
                empty.transform.position = this.transform.position;
                this.transform.parent = empty.transform;

                this.transform.DOPunchScale(new Vector3(0.4f, 0.4f, 0.4f), 0.6f, 5);                

                sequenceImpactShader = DOTween.Sequence();

                sequenceImpactShader.Join(veg1Mat.DOFloat(25, "_Glow", 0.3f));
                sequenceImpactShader.Join(veg2Mat.DOFloat(25, "_Glow", 0.3f));
                sequenceImpactShader.Join(veg3Mat.DOFloat(25, "_Glow", 0.3f));
                sequenceImpactShader.Join(veg4Mat.DOFloat(25, "_Glow", 0.3f));

                sequenceImpactShader.OnComplete(() =>
                {
                    sequenceImpactShader.Join(veg1Mat.DOFloat(10, "_Glow", 0.3f));
                    sequenceImpactShader.Join(veg2Mat.DOFloat(10, "_Glow", 0.3f));
                    sequenceImpactShader.Join(veg3Mat.DOFloat(10, "_Glow", 0.3f));
                    sequenceImpactShader.Join(veg4Mat.DOFloat(10, "_Glow", 0.3f));
                });

                part2Enter = false;
            }
            if (Time.time - startTime >= 4 + 1)
            {
                if (part3Enter)
                {
                    veg1.transform.DOScale(Vector3.zero, 0.7f);
                    veg2.transform.DOScale(Vector3.zero, 0.7f);
                    veg3.transform.DOScale(Vector3.zero, 0.7f);
                    veg4.transform.DOScale(Vector3.zero, 0.7f);

                    bulletInstance = GameObject.Instantiate(bullet, this.transform);
                    bulletInstance.GetComponent<VenomBulletScript>().up = false;
                    bulletInstance.transform.rotation = Quaternion.Euler(0, 0, 0);
                    bulletInstance.transform.localRotation = Quaternion.Euler(0, 0, 0);

                    Vector3 aux = new Vector3(0, 6, 0);
                    bulletInstance.transform.localPosition = aux;

                    Color srColor = bulletInstance.GetComponentInChildren<SpriteRenderer>().color;
                    srColor.a = 0;
                    bulletInstance.GetComponentInChildren<SpriteRenderer>().color = srColor;

                    bulletInstance.GetComponentInChildren<SpriteRenderer>().DOFade(1, 0.7f);

                    bulletInstance.transform.DOLocalMoveY(0, 0.7f);

                    part3Enter = false;

                    veg1.transform.DOScale(Vector3.zero, 0.7f);
                    veg2.transform.DOScale(Vector3.zero, 0.7f);
                    veg3.transform.DOScale(Vector3.zero, 0.7f);
                    veg4.transform.DOScale(Vector3.zero, 0.7f);
                }
            }                      

        }
    }
}
