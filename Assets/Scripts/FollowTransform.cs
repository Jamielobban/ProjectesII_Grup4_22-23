using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FollowTransform : MonoBehaviour
{
    private void Update()
    {
        Transform[] childs = GetComponentsInChildren<Transform>().Where(t => t.GetComponent<PlayerMovement>()).ToArray();

        if (childs.Length > 0)
        {
            childs[0].localPosition = Vector3.zero;
        }

    }

    //private void OnDisable()
    //{
    //    Transform[] childs = GetComponentsInChildren<Transform>().Where(t => t.GetComponent<PlayerMovement>()).ToArray();

    //    if (childs.Length > 0)
    //    {
    //        childs[0].parent = null;
    //    }
    //}

}
