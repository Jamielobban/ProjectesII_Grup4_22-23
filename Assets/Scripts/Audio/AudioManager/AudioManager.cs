using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource sfxAudioSource, musicAudioSource, sfxLoop;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        
    }

    public void PlaySound(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }
    public void PlaySound(AudioClip clip, float delay)
    {
        sfxAudioSource.clip = clip;
        sfxAudioSource.PlayDelayed(delay);
    }
    public AudioSource PlaySoundLoop(AudioClip clip)
    {
        sfxLoop.PlayOneShot(clip);
        return sfxLoop;
    }

    private void ToggleMusic()
    {
        musicAudioSource.mute = !musicAudioSource.mute;
    }
}
