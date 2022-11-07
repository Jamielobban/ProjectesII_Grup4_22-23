using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    Vector2 mousePos;
    public Camera cam;

    //float weaponPosX = 0.65f, weaponPosZ = 0;
   //[SerializeField] int Right1_Left_Minus1;

    // Update is called once per frame
    void Update()
    {
        cam = Camera.main;
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos);

        Vector2 direc = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = -direc;

        //float anglePlayer = Mathf.Atan2(direc.y, direc.x) * Mathf.Rad2Deg;
        //Debug.Log(anglePlayer);

        //this.transform.localPosition = new Vector3(weaponPosX, -0.0f, weaponPosZ);

        //if (anglePlayer > 90 || anglePlayer < -90)
        //{
        //    weaponPosX = -0.65f /** Right1_Left_Minus1*/;
        //}
        //else
        //{
        //    weaponPosX = 0.65f /** Right1_Left_Minus1*/;

        //}

        //if (angle < 0)
        //{
        //    weaponPosZ = -0.5f;
        //}
        //else
        //{
        //    weaponPosZ = 0.5f;
        //}
    }
}