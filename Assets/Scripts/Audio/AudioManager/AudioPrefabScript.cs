using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPrefabScript : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    public BoolValue sfxEnabled;
    public BoolValue musciEnabled;
    public FloatValue sfxValue;
    public FloatValue musciValue;

    public float defaultSoundValue = 1;
    public bool amIsfx = true;

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
        else
        {
            if (amIsfx)
            {
                if (sfxEnabled.RuntimeValue)
                {
                    audioSource.volume = defaultSoundValue * sfxValue.RuntimeValue;
                }
                else
                {
                    audioSource.volume = 0;
                }
            }
            else
            {
                if (musciEnabled.RuntimeValue)
                {
                    audioSource.volume = defaultSoundValue * musciValue.RuntimeValue;
                }
                else
                {
                    audioSource.volume = 0;
                }
            }
        }

    }
}
