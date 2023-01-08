using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    bool check;
    // Start is called before the first frame update
    void Start()
    {
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        check = true;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& check)
        {
            collision.gameObject.SendMessage("GetDamage", 10);
            check = false;
            StartCoroutine(Delay(0.5f));
        }
    }
}
