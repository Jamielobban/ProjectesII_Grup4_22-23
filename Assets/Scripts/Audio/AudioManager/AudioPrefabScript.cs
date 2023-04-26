using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPrefabScript : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    BoolValue gamePaused;
    public BoolValue sfxEnabled;
    public BoolValue musciEnabled;
    public FloatValue sfxValue;
    public FloatValue musciValue;
    public AudioMixerSnapshot unpaused;
    public AudioMixerSnapshot paused;


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

        if(audioSource.isPlaying && gamePaused.RuntimeValue && amIsfx)
        {
            audioSource.Pause();
        }

        if (!audioSource.isPlaying && playing)
        {            
            if(audioSource.time == audioSource.clip.length)
            { 
                //auido terminado
                if (myId.HasValue)
                {
                    AudioManager.Instance.RemoveAudio(myId.Value);
                }
            }
            else if(!gamePaused.RuntimeValue && amIsfx)
            {
                audioSource.UnPause();
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
