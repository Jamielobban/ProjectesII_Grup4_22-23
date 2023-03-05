using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burbuja : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay(Random.RandomRange(4, 8)));
    }

    private IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        this.GetComponent<Animator>().SetTrigger("Explotar");
        StartCoroutine(Delay(Random.RandomRange(4, 8)));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
