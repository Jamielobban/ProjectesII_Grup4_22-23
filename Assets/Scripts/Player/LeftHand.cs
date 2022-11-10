using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    Vector2 mousePos;
    Camera cam;

    float weaponPosX = 0.65f, weaponPosZ = 0;

    public Transform firePoint;
    public float lifeTime;
    public float startTime;
    public Weapon weaponInHand;
    public GameObject rotatePoint;
    //private Transform asd;
    
    // Update is called once per frame
    private void Start()
    {
        rotatePoint = GameObject.FindGameObjectWithTag("RotatePoint");
        cam = Camera.main;        
        
    }
    void Update()
    {
        //asd = rotatePoint.transform;
        //rotatePoint = FindObjectOfType<Transform>();
        
        
        this.transform.SetParent(rotatePoint.transform,false);
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos);

        Vector2 direc = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = -direc;

        //float anglePlayer = Mathf.Atan2(direc.y, direc.x) * Mathf.Rad2Deg;
        ////Debug.Log(anglePlayer);

        //Vector3 playerPos = cam.GetComponent<Cinemachine.CinemachineBrain>().ActiveVirtualCamera.Follow.transform.localPosition;

        //this.transform.localPosition = new Vector3(weaponPosX + playerPos.x, -0.0f + playerPos.y, weaponPosZ);

        //if (anglePlayer > 90 || anglePlayer < -90)
        //{
        //    weaponPosX = 0.65f /** Right1_Left_Minus1*/;
        //}
        //else
        //{
        //    weaponPosX = -0.65f /** Right1_Left_Minus1*/;

        //}

        ////if (angle < 0)
        ////{
        ////    weaponPosZ = -0.5f;
        ////}
        ////else
        ////{
        ////    weaponPosZ = 0.5f;
        ////}
    }
    
}
