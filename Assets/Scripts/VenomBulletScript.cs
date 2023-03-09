using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VenomBulletScript : MonoBehaviour
{
    [SerializeField]
    GameObject gas;
    public bool up = true;
    [HideInInspector]
    public float startTime;
    GameObject gasInstance;
    bool done = false;
    
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= 0.7f && !up && !done)
        {
            gasInstance = Instantiate(gas, this.transform.position, Quaternion.identity);
            GetComponentInChildren<SpriteRenderer>().DOFade(0, 0.5f);
            FunctionTimer.Create(() =>
            {
                if(gasInstance != null)
                {
                    gasInstance.GetComponentInChildren<Animator>().SetBool("stopLoop", true);
                }
                if (this.gameObject != null)
                {
                    Destroy(this.gameObject);
                }
            },3.8f);
            done = true;
        }
    }
}
