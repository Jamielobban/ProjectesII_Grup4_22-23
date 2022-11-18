using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject x;
    public GameObject player;

    Vector3 pos;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pos = x.transform.position + ((player.transform.position - x.transform.position) / 2);
        this.transform.position = pos;
    }
}
