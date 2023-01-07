using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPrefabScript : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    bool playing = false;
    
    public int? myId = null;

    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {

        if (audioSource.isPlaying && !playing)
        {
            playing = true;
        }
        if (!audioSource.isPlaying && playing)
        {
            if (myId.HasValue)
            {
                AudioManager.Instance.RemoveAudio(myId.Value);
            }
        }
    }
}
