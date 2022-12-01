using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPrefabScript : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    bool playing = false;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {        

        if(audioSource.isPlaying && !playing)
        {
            playing = true;
        }
        if(!audioSource.isPlaying && playing)
        {
            FindObjectOfType<AudioManager>().RemoveFromDictionary(this.gameObject.GetInstanceID());
            Destroy(this.gameObject);
        }
    }
}
