using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    public GameObject arrow;
    public List<GameObject> arrows = new List<GameObject>();
    public Transform spawnPoint;
    public Transform end;

    public bool up;
    private int dir;
    int arrowsNum;
    float rotation;
    // Start is called before the first frame update
    void Start()
    {
        arrowsNum = 0;
        StartCoroutine(shoot());

        if(up)
        {
            dir = 1;
            rotation = 180;
        }
        else
        {
            dir = -1;
            rotation = 0;

        }

    }

    private IEnumerator shoot()
    {
        yield return new WaitForSeconds(Random.RandomRange(1f, 2f));

        arrows.Add(Instantiate(arrow, spawnPoint.position, new Quaternion(spawnPoint.rotation.x, spawnPoint.rotation.y, rotation,0)) as GameObject);

        arrows[arrowsNum].GetComponent<Rigidbody2D>().AddForce(dir*transform.up * 1500);
        arrows[arrowsNum].transform.SetParent(this.gameObject.transform);
        arrowsNum++;
        StartCoroutine(shoot());

    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        for (int i = 0; i < arrowsNum; i++)
        {
            if (Vector3.Distance(arrows[i].transform.position, end.position) < 0.5f)
            {
                arrows[i].GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                arrows[i].gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Stop");
                Destroy(arrows[i].gameObject, 1f);
                arrows.Remove(arrows[i]);     

                arrowsNum--;
            }
        }
    }
}
