using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomLoopAnimation());
        
    }
    private IEnumerator RandomLoopAnimation()
    {
        yield return new WaitForSeconds(Random.RandomRange(3,6));
        this.GetComponent<Animator>().SetTrigger("Anim");

        StartCoroutine(RandomLoopAnimation());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
