using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : Trampas
{
    public GameObject arrow;
    public Transform spawnPoint;
    public Transform end;
    public GameObject list;

    [SerializeField]
    AudioClip arrowThrow;
    [SerializeField]
    AudioClip arrowAir;
    [SerializeField]
    AudioClip arrowHit;

    int? arrowThrowKey;
    int? arrowAirKey;
    int? arrowHitKey;

    public float time;


    public float startTime;
    bool a;
    // Start is called before the first frame update
    void Start()
    {
        a = true;
            StartCoroutine(shoot(startTime));



    }
    public override void StartSpawning()
    {
        a = true;

        

    }
    private IEnumerator shoot(float time)
    {
            yield return new WaitForSeconds(time);

        if (a)
        {



            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Shoot");
        }

            yield return new WaitForSeconds(0.15f);
        if(a)
        { 
            arrowThrowKey = AudioManager.Instance.LoadSound(arrowThrow, this.transform, 0, false);
            GameObject flecha = Instantiate(arrow, spawnPoint.position, spawnPoint.rotation);

            arrowAirKey = AudioManager.Instance.LoadSound(arrowAir, flecha.transform, 0, false);

            flecha.GetComponent<Rigidbody2D>().AddForce(transform.up * -1500);
            flecha.transform.SetParent(list.gameObject.transform);

        }

        if (this.time == 0)
        {
            StartCoroutine(shoot(Random.RandomRange(1f, 2f)));

        }
        else
        {
            StartCoroutine(shoot(this.time));

        }
        
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (stopSpawning)
        {
            stopSpawning = false;
            a = false;
        }
        for (int i = 0; i < list.transform.childCount; i++)
        {
            if (Vector3.Distance(list.transform.GetChild(i).transform.position, end.position) < 0.5f)
            {
                arrowHitKey = AudioManager.Instance.LoadSound(arrowHit, list.transform.GetChild(i).transform.position, 0, false);
                list.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                list.transform.GetChild(i).gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Stop");
                Destroy(list.transform.GetChild(i).gameObject.GetComponent<CircleCollider2D>());
                Destroy(list.transform.GetChild(i).gameObject, 1f);

            }

            if (list.transform.GetChild(i).gameObject == null)
            {
                arrowHitKey = AudioManager.Instance.LoadSound(arrowHit, list.transform.GetChild(i).position, 0, false);

            }
        }
    }
}
