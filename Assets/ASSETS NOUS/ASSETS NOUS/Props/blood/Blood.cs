using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    [SerializeField]
    AudioClip gotaSound;

    int? gotaSoundKey;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RandomLoopAnimation());
        
    }
    private IEnumerator RandomLoopAnimation()
    {
        yield return new WaitForSeconds(Random.RandomRange(3,6));
        //gotaSoundKey = AudioManager.Instance.LoadSound(gotaSound, this.transform, 0, false);

        this.GetComponent<Animator>().SetTrigger("Anim");

        StartCoroutine(RandomLoopAnimation());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
