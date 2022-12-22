using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : MonoBehaviour
{
    public GameObject log;
    public Transform end;
    public List<GameObject> logs = new List<GameObject>();

    public Transform spawnPoint;
    int logsNum;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(animation(3f));
        logsNum = 0;
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


        logs.Add(Instantiate(log, spawnPoint.position, spawnPoint.rotation) as GameObject);
        logs[logsNum].GetComponent<Rigidbody2D>().AddForce(-transform.up*500);
        logs[logsNum].transform.SetParent(this.gameObject.transform.parent);

        logsNum++;

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        for(int i = 0; i < logsNum; i++)
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
