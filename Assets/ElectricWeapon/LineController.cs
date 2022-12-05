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

    private ElectricConnection ec;
    private float distanceBetween;


    private Vector3 startPositionVec;
    private void Start()
    {
        ec = FindObjectOfType<ElectricConnection>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    public void AssignTarget(Vector3 startPosition, Transform newTarget)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, startPosition);
        startPositionVec = startPosition;
        target = newTarget;
    }
    private void Update()
    {
        if(ec.allEnemies == null || ec.allEnemies.Count == 0) 
        {
            return;
        }
        else
        {

            ec.isConnected = true;
            lineRenderer.SetPosition(1, target.position);
            GenerateMeshCollider();
        }
        //}

        distanceBetween = Vector3.Distance(startPositionVec,target.position);

        //Debug.Log(distanceBetween);
        if(distanceBetween >= 10.0f)
        {
            ec.isConnected = false;
            ec.allLines.Clear();
            Destroy(this.gameObject);
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
    public void GenerateMeshCollider()
    {
        MeshCollider collider = GetComponent<MeshCollider>();

        if(collider == null)
        {
            collider = gameObject.AddComponent<MeshCollider>();
        }
        Mesh mesh = new Mesh();
        lineRenderer.BakeMesh(mesh);
        collider.sharedMesh = mesh;
    }
}

