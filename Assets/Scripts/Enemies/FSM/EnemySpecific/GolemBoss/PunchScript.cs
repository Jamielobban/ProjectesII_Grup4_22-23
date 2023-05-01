using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{
    float startTime;
    [SerializeField]
    GameObject crackManager;
    [SerializeField]
    Transform topParent;
    


    void OnEnable()
    {
        startTime = Time.time;
        GameObject crackManagerInstance = GameObject.Instantiate(crackManager, this.transform.position, Quaternion.identity);
        crackManagerInstance.GetComponentInChildren<SpawnObjectsInCircle>().player = GetComponentInParent<Enemy15>().player;
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= 0.75f)
        {
            this.gameObject.SetActive(false);
        }
    }
}
