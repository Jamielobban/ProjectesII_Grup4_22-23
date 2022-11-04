using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    Vector2 mousePos;
    public Camera cam;

    float weaponPosX = 0.5f, weaponPosZ = 0;

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(mousePos);

        Vector2 direc = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direc;

        float anglePlayer = Mathf.Atan2(direc.y, direc.x) * Mathf.Rad2Deg;
        //Debug.Log(anglePlayer);

        this.transform.localPosition = new Vector3(weaponPosX, -0.5f, weaponPosZ);

        if (anglePlayer > 90 || anglePlayer < -90)
        {
            weaponPosX = -0.5f;
        }
        else
        {
            weaponPosX = 0.5f;

        }

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