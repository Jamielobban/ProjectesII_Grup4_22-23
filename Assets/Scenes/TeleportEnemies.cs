using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEnemies : MonoBehaviour
{
    public GameObject[] enemies;
    public float a;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(check());
    }

    // Update is called once per frame
    void Update()
    {


    }
    private IEnumerator check()
    {
        yield return new WaitForSeconds(0.5f);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for(int i = 0; i <enemies.Length; i++)
        {
            a = Vector3.Distance(enemies[i].transform.position, this.transform.GetChild(0).transform.position);
            if (Vector3.Distance(enemies[i].transform.position, this.transform.GetChild(0).transform.position) > 20)
            {
                enemies[i].transform.position = this.transform.GetChild(0).transform.position;
            }
        }
        StartCoroutine(check());

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

    }
}
