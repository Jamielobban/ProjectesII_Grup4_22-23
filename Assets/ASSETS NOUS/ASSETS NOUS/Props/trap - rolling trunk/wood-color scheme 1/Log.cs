using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public GameObject log;

    public Transform spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(animation(3f));

    }

    private IEnumerator animation(float time)
    {
        yield return new WaitForSeconds(time);
        this.GetComponent<Animator>().SetTrigger("Triger");
        StartCoroutine(Spawn(0.6f));

        StartCoroutine(animation(3f));
    }

    private IEnumerator Spawn(float time)
    {
        yield return new WaitForSeconds(time);

        
        GameObject logs = Instantiate(log, spawnPoint.position, spawnPoint.rotation);
        logs.GetComponent<Rigidbody2D>().AddForce(-transform.up*500);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
