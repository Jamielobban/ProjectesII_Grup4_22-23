using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamBosque : MonoBehaviour
{
    Sequence sq;

    float enterTime;
    bool travelCenter = false, magnified = false;

    [SerializeField]
    Cinemachine.CinemachineVirtualCamera cam;
    [SerializeField]
    Transform transform;
    [SerializeField]
    GameObject square;
    [SerializeField]
    GameObject square2;
    Color col;
    Color col2;

    void Start()
    {
        transform.position = new Vector3(20.9f, 9.7f, -10);
        cam.m_Lens.OrthographicSize = 0f;

        enterTime = Time.time;
        col = square2.GetComponent<SpriteRenderer>().color;
        col.a = 0;

        col2 = square2.GetComponent<SpriteRenderer>().color;
        col2.a = 1;
        square.GetComponent<SpriteRenderer>().color = col2;

        square.GetComponent<SpriteRenderer>().material.SetFloat("_FadeAmount", 0.846f);

    }

    // Update is called once per frame
    void Update()
    {
        square2.GetComponent<SpriteRenderer>().color = col;

        if (Time.time - enterTime >= 1 && !magnified)
        {
            magnified = true;
            DOVirtual.Float(cam.m_Lens.OrthographicSize, 15f, 2.5f, (value) => {
                cam.m_Lens.OrthographicSize = value;
            });
        }

        if(Time.time - enterTime >= 4 && !travelCenter)
        {
            DOVirtual.Float(cam.m_Lens.OrthographicSize, 24.5f, 5, (value) => {
                cam.m_Lens.OrthographicSize = value;
            });
            DOVirtual.Float(col.a, 1, 10, (value) =>
            {
                col.a = value;
            });
            square.GetComponent<SpriteRenderer>().material.DOFloat(0.138f, "_FadeAmount", 10);
            transform.DOMove(new Vector3(11.3f, 90.2f, -10), 5);
            cam.transform.DOShakePosition(10);
            travelCenter = true;
        }
    }//0.138
}
