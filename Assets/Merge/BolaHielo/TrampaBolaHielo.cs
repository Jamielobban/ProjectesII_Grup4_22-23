using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaBolaHielo : MonoBehaviour
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


    public int force;

    public bool stopSpawning;
    // Start is called before the first frame update
    void Start()
    {

        stopSpawning = false;


    }

    public void StartSpawning()
    {
        StartCoroutine(shoot());

    }
    private IEnumerator shoot()
    {
        if (time == 0)
        {
            yield return new WaitForSeconds(Random.RandomRange(1f, 2f));
        }
        else
        {
            yield return new WaitForSeconds(time);
        }

        arrowThrowKey = AudioManager.Instance.LoadSound(arrowThrow, this.transform, 0, false);
        GameObject bola = Instantiate(arrow, spawnPoint.position, spawnPoint.rotation);

        arrowAirKey = AudioManager.Instance.LoadSound(arrowAir, bola.transform, 0, false);

        bola.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
        bola.transform.SetParent(list.gameObject.transform);
        if (stopSpawning)
        {
            stopSpawning = false;
        }
        else
        {
            StartCoroutine(shoot());

        }

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        for (int i = 0; i < list.transform.childCount; i++)
        {
            if (Vector3.Distance(list.transform.GetChild(i).position, end.position) < 0.5f)
            {
                arrowHitKey = AudioManager.Instance.LoadSound(arrowHit, list.transform.GetChild(i).position, 0, false);
                list.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                list.transform.GetChild(i).gameObject.transform.GetComponent<Animator>().SetTrigger("Stop");
                Destroy(list.transform.GetChild(i).gameObject, 1f);
            }

            if (list.transform.GetChild(i).gameObject == null)
            {
                arrowHitKey = AudioManager.Instance.LoadSound(arrowHit, list.transform.GetChild(i).position, 0, false);

            }
        }
    }
}
