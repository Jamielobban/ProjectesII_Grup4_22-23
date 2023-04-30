using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackScript : MonoBehaviour
{
    Vector2 directionVector;
    float enterTime;
    [SerializeField]
    float timeBetweenCracks;
    [SerializeField]
    float timeCrackActive;
    bool nextCreated = false;
    GameObject crack;
    bool spawn = true;
    void Start()
    {
        crack = Resources.Load<GameObject>("Prefab/Enemies/GroundCrack");
        directionVector = (this.transform.position - transform.parent.position).normalized;
        enterTime = Time.time;
        this.transform.parent = null;
    }

    private void Update()
    {
        if(Time.time - enterTime >= timeBetweenCracks && nextCreated == false && spawn)
        {
            GameObject nextCr = Instantiate(crack, this.transform.position + (Vector3)directionVector * 2, Quaternion.identity);
            nextCr.transform.parent = this.transform;
            nextCreated = true;
        }

        if(Time.time - enterTime >= timeCrackActive)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MapLimit"))
        {
            spawn = false;
        }
    }

}
