using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GasScript : MonoBehaviour
{
    // Start is called before the first frame update
    float lastHitTime = 0;
    //[SerializeField]
    //LayerMask layerToColide;

    void Start()
    {
        //97
        Vector3 actRotation = transform.rotation.eulerAngles;
        this.transform.DORotate(new Vector3(150, actRotation.y, actRotation.z), 2.5f);
    }    
    
}