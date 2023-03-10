using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class VenomBulletScript : MonoBehaviour
{
    [SerializeField]
    GameObject gas;    
    public AudioClip fallingSound;
    public AudioClip acidHit;
    public AudioClip acidBubbles;
    public bool up = true;
    [HideInInspector]
    public float startTime;
    GameObject gasInstance;
    bool done = false;
    int? fallingKey;
    
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!up && !fallingKey.HasValue)
        {
            fallingKey = AudioManager.Instance.LoadSound(fallingSound, this.transform);
            AudioManager.Instance.GetAudioFromDictionaryIfPossible(fallingKey.Value).pitch = 1.7f;
        }

        if (Time.time - startTime >= 0.7f && !up && !done)
        {
            gasInstance = Instantiate(gas, this.transform.position, Quaternion.identity);
            AudioManager.Instance.LoadSound(acidHit, this.transform.position);
            AudioManager.Instance.LoadSound(acidBubbles, gasInstance.transform);
            GetComponentInChildren<SpriteRenderer>().DOFade(0, 0.5f);
            FunctionTimer.Create(() =>
            {
                if(gasInstance != null)
                {
                    gasInstance.GetComponentInChildren<Animator>().SetBool("stopLoop", true);
                }
                if (this.gameObject != null)
                {
                    Destroy(this.gameObject);
                }
            },3.8f);
            done = true;
        }
    }
}
