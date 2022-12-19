using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    public GameObject blood;

    float currentWaitTime;
    GameObject bloods1, bloods2, bloods3;

    bool disapear = false;
    float alpha = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        bloods1 = Instantiate(blood, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));




        currentWaitTime = Time.realtimeSinceStartup;
        StartCoroutine(disappear(4));



    }

    private IEnumerator disappear(float time)
    {
        yield return new WaitForSeconds(time);

        disapear = true;
       
    }
    // Update is called once per frame
    void Update()
    {
        if(disapear == true)
        {


            alpha -= 0.1f * Time.deltaTime;


            bloods1.GetComponent<SpriteRenderer>().color = new Color(bloods1.GetComponent<SpriteRenderer>().color.r, bloods1.GetComponent<SpriteRenderer>().color.g, bloods1.GetComponent<SpriteRenderer>().color.b, bloods1.GetComponent<SpriteRenderer>().color.a*alpha);
        


            if (alpha < 0)
            {
                Destroy(bloods1);

                Destroy(transform.parent.gameObject);
            }

        }
    }
}