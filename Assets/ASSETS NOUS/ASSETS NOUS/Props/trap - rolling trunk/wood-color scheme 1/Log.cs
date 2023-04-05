using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Trampas
{
    public GameObject log;
    public Transform end;
    public List<GameObject> logs = new List<GameObject>();

    [SerializeField]
    AudioClip doorLogSound;

    int? logDoorKey;

    public Transform spawnPoint;
    int logsNum;
    bool a;
    // Start is called before the first frame update
    void Start()
    {
        a = true;
        logsNum = 0;
        StartCoroutine(animation(0f));

    }
    public override void StartSpawning()
    {
        a = true;
    }
    private IEnumerator animation(float time)
    {
            yield return new WaitForSeconds(time);

        if (a)
        {
            logDoorKey = AudioManager.Instance.LoadSound(doorLogSound, this.transform, 0, false);
            this.GetComponent<Animator>().SetTrigger("Triger");

             StartCoroutine(Spawn(0.6f));

        }

            StartCoroutine(animation(Random.Range(2.0f, 3.0f)));
        
    }

    private IEnumerator Spawn(float time)
    {
        if (a)
        {
            yield return new WaitForSeconds(time);


            logs.Add(Instantiate(log, spawnPoint.position, spawnPoint.rotation) as GameObject);
            logs[logsNum].GetComponent<Rigidbody2D>().AddForce(-transform.up * 500);
            logs[logsNum].transform.SetParent(this.gameObject.transform.parent);

            logsNum++;
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (stopSpawning)
        {
            a = false;
            stopSpawning = false;
        }

        for (int i = 0; i < logsNum; i++)
        {
            if(Vector3.Distance(logs[i].transform.position,end.position) < 0.1f)
            {
                logs[i].gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("End");
                Destroy(logs[i], 1f);
                logs.Remove(logs[i]);
                logsNum--;
            }
        }
    }
}
