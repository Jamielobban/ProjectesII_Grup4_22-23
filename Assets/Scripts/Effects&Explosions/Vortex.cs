using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    [SerializeField]
    float timeActive;
    [SerializeField]
    AudioClip vortexAbsorb;
    [SerializeField]
    AudioClip vortexOff;
    [SerializeField]
    AudioSource absorbAudioSource;
    Rigidbody2D rb;
    CircleCollider2D cc;
    PointEffector2D pe;
    SpriteRenderer sr;
    Dictionary<int, Collider2D> enemiesInside = new Dictionary<int, Collider2D>();
    bool timeCompleted = false;
    float startTimeVortex;
    // Start is called before the first frame update
    void Start()
    {
        startTimeVortex = Time.time;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.enabled = true;
        pe = GetComponent<PointEffector2D>();
        pe.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCompleted)
        {
            foreach (KeyValuePair<int, Collider2D> attachStat in enemiesInside)
            {
                
                enemiesInside[attachStat.Key].attachedRigidbody.velocity *= new Vector3(0, 0, 0);
            }
            absorbAudioSource.PlayOneShot(vortexOff);
            Destroy(this.gameObject);
        }

        if(Time.time - startTimeVortex >= timeActive)
        {
            timeCompleted = true;
            pe.enabled = false;
            sr.enabled = false;
            foreach (KeyValuePair<int, Collider2D> attachStat in enemiesInside)
            {
                enemiesInside[attachStat.Key].gameObject.transform.localScale = new Vector3(1, 1, 1);                
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!timeCompleted)
            {
                if (!enemiesInside.ContainsKey(collision.gameObject.GetInstanceID()))
                {
                    enemiesInside.Add(collision.gameObject.GetInstanceID(), collision);
                }
                Vector3 vectorDistance = collision.gameObject.transform.position - this.transform.position;
                float magnitudeDistance = Mathf.Sqrt(Mathf.Pow(vectorDistance.x, 2) + Mathf.Pow(vectorDistance.y, 2));
                float enemyScale = Mathf.Lerp(0, 1, magnitudeDistance / cc.radius);//cc.radius / Mathf.Abs(Vector3.Distance(collision.attachedRigidbody.position, rb.position))
                //Debug.Log(magnitudeDistance / cc.radius);
                collision.gameObject.transform.localScale = new Vector3(enemyScale, enemyScale, 1);
                if(enemyScale <= 0.1f)
                {
                    enemiesInside.Remove(collision.gameObject.GetInstanceID());
                    collision.gameObject.GetComponent<EnemyController>().isDeath = true;
                    absorbAudioSource.PlayOneShot(vortexAbsorb);
                    timeActive++;
                    this.transform.localScale *= 1.2f;
                }
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.attachedRigidbody.velocity = Vector3.zero;
    }


}
