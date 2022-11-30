using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Texture[] textures;

    private int animationStep;

    [SerializeField]
    private float fps = 30f;

    private float fpsCounter;

    private Transform target;

    private ElectricDistanceCheck ec;
    private void Awake()
    {
        ec = FindObjectOfType<ElectricDistanceCheck>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void AssignTarget(Vector3 startPosition, Transform newTarget)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPosition);
        target = newTarget;
    }
    private void Update()
    {
        if (ec.isInRange)
        {
            lineRenderer.SetPosition(1, target.position);
        }


        fpsCounter += Time.deltaTime;
        if (fpsCounter >= 1f / fps)
        {
            animationStep++;
            if (animationStep == textures.Length)
            {
                animationStep = 0;
            }
            lineRenderer.material.SetTexture("_MainTex", textures[animationStep]);

            fpsCounter = 0f;
        }
    }
}

