using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System.Linq;

public class SwordMissileManager : MonoBehaviour
{
    //Rigidbody2D rb;
    public float initialSpeed = 20f;        
    public Transform player;
    public Transform[] swordsChilds;

    float startTime;
    float[] timeToThrow = new float[4];
    bool[] swordThrowed = new bool[4];
    bool[] swordEnded = new bool[4];
    

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
        swordsChilds = GetComponentsInChildren<NavMeshAgent>().Select(na => na.transform).ToArray();       
        
        

        for (int i = 0; i < 4; i++)
        {
            timeToThrow[i] = Random.Range(0.3f, 0.75f);
            swordThrowed[i] = false;
            swordEnded[i] = false;
            swordsChilds[i].transform.parent = null;
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), Mathf.Abs(this.transform.localScale.y), Mathf.Abs(this.transform.localScale.z));
            swordsChilds[i].localScale = new Vector3(Mathf.Abs(swordsChilds[i].localScale.x), Mathf.Abs(swordsChilds[i].localScale.y), Mathf.Abs(swordsChilds[i].localScale.z));
            swordsChilds[i].GetComponent<Rigidbody2D>().AddForce(swordsChilds[i].transform.right * initialSpeed, ForceMode2D.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
       for(int i = 0; i < 4; i++)
        {

            if(!swordEnded[i] && (Time.time - (startTime + timeToThrow[i]) >= 4))
            {
                swordEnded[i] = true;
                swordsChilds[i].GetComponentInChildren<SpriteRenderer>().material.DOFade(0, 0.3f);
                Destroy(swordsChilds[i].gameObject, 0.3f);
                int swordsEnded = 0;
                for(int j = 0; j < 4; j++)
                {
                    if (swordEnded[j])
                    {
                        swordsEnded++;
                    }                    
                }
                if(swordsEnded >= 4)
                {
                    Destroy(this.gameObject, 0.3f);
                }
            }

            if(Time.time - startTime >= timeToThrow[i])
            {
                if(!swordEnded[i])
                {
                    if (!swordsChilds[i].GetComponent<NavMeshAgent>().enabled)
                    {
                        swordsChilds[i].GetComponent<NavMeshAgent>().enabled = true;
                    }
                    swordsChilds[i].GetComponent<NavMeshAgent>().destination = player.position;
                    swordThrowed[i] = true;
                    
                }
                
            }
       }

        for (int i = 0; i < 4; i++)
        {
            if (swordsChilds[i] != null && swordsChilds[i].gameObject.activeInHierarchy && swordsChilds[i].GetComponent<NavMeshAgent>().enabled && swordsChilds[i].gameObject.activeInHierarchy)
            {


                Vector3 movementDirection = swordsChilds[i].GetComponent<NavMeshAgent>().velocity.normalized;
                
                float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
                swordsChilds[i].rotation = Quaternion.Euler(0f, 0f, angle);
                            

            }

        }

    }


}
