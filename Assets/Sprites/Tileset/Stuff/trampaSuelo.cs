using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampaSuelo : Trampas
{

    [SerializeField]
    AudioClip up;
    [SerializeField]
    AudioClip down;


    int? up1;
    int? down1;

    public float timeUp;
    public float timeDown;


    public float startTime;

    public BoxCollider2D getDamage;

    // Start is called before the first frame update
    void Start()
    {
        getDamage.enabled = false;
        StartCoroutine(Open(startTime));

    }
    public override void StartSpawning()
    {
        this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Spawn", true);


    }

    private IEnumerator damage()
    {

        yield return new WaitForSeconds(0.3f);

        getDamage.enabled = true;


    }
    private IEnumerator endDamage()
    {

        yield return new WaitForSeconds(0.3f);

        getDamage.enabled = false;


    }
    private IEnumerator Open(float time)
    {

        yield return new WaitForSeconds(time);

        this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Up", true);

        up1 = AudioManager.Instance.LoadSound(up, this.transform, 0, false);
        StartCoroutine(damage());

        yield return new WaitForSeconds(timeUp);
        this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Up", false);



        down1 = AudioManager.Instance.LoadSound(down, this.transform, 0, false);
        StartCoroutine(endDamage());




       
            StartCoroutine(Open(timeDown));
        
    }
    // Update is called once per frame
    void Update()
    {
        if (stopSpawning)
        {

            this.gameObject.transform.GetChild(0).GetComponent<Animator>().SetBool("Spawn", false);

            stopSpawning = false;
        }
    }
}
