using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip[] lavaAudios;
    [SerializeField] int? audioKey;
    
    [SerializeField] int audioToPlay;
   
    void Start()
    {
       
        InvokeRepeating("ChangeAudioAndPlace", 0f, Random.Range(3,7));
    }

    void ChangeAudioAndPlace()
    {
        Debug.Log("Repeat");
        audioToPlay = Random.Range(0, lavaAudios.Length);
        audioKey = AudioManager.Instance.LoadSound(lavaAudios[audioToPlay], this.transform, 0, false, true, MixerGroups.ENVIRONMENT,1);
    }
}
