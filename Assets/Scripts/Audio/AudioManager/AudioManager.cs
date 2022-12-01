using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //[SerializeField] AudioSource sfxAudioSource, musicAudioSource, sfxLoop;

    [SerializeField] GameObject audioSourcePrefab;

    Dictionary<int, KeyValuePair<float, KeyValuePair<GameObject, AudioClip>>> instantiatedPrefabAtTimeAndPositionWithAudioclip = new Dictionary<int, KeyValuePair<float, KeyValuePair<GameObject, AudioClip>>>();

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

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        GameObject clone = Instantiate(audioSourcePrefab, pos, Quaternion.identity);
        clone.GetComponent<AudioSource>().PlayOneShot(clip);

        KeyValuePair<GameObject,AudioClip> cloneWithAudiclip = new KeyValuePair<GameObject, AudioClip>(clone, clip);
        instantiatedPrefabAtTimeAndPositionWithAudioclip.Add(clone.GetInstanceID(), new KeyValuePair<float, KeyValuePair<GameObject, AudioClip>> (Time.time, cloneWithAudiclip));
    }

    public void PlaySound(AudioClip clip, Transform parentToSetPrefab)
    {
        GameObject clone = Instantiate(audioSourcePrefab,parentToSetPrefab);
        clone.GetComponent<AudioSource>().PlayOneShot(clip);

        KeyValuePair<GameObject, AudioClip> cloneWithAudiclip = new KeyValuePair<GameObject, AudioClip>(clone, clip);
        instantiatedPrefabAtTimeAndPositionWithAudioclip.Add(clone.GetInstanceID(), new KeyValuePair<float, KeyValuePair<GameObject, AudioClip>>(Time.time, cloneWithAudiclip));
    }



    public void PlaySoundDelayed(AudioClip clip, float delay, Vector3 pos)
    {
        GameObject clone = Instantiate(audioSourcePrefab, pos, Quaternion.identity);
        clone.GetComponent<AudioSource>().clip = clip;

        clone.GetComponent<AudioSource>().PlayDelayed(delay);

        WaitToAddInToDictionary(clip, delay, clone);
        
    }

    public void PlaySoundDelayed(AudioClip clip, float delay, Transform parentToSetPrefab)
    {
        GameObject clone = Instantiate(audioSourcePrefab, parentToSetPrefab);
        clone.GetComponent<AudioSource>().clip = clip;

        clone.GetComponent<AudioSource>().PlayDelayed(delay);

        WaitToAddInToDictionary(clip, delay, clone);

    }


    //public AudioSource PlaySoundLoop(AudioClip clip)
    //{
    //    sfxLoop.PlayOneShot(clip);
    //    return sfxLoop;
    //}

    //private void ToggleMusic()
    //{
    //    musicAudioSource.mute = !musicAudioSource.mute;
    //}

    IEnumerator WaitToAddInToDictionary(AudioClip clip, float delay, GameObject clone)
    {
        yield return new WaitForSeconds(delay);

        KeyValuePair<GameObject, AudioClip> cloneWithAudiclip = new KeyValuePair<GameObject, AudioClip>(clone, clip);
        instantiatedPrefabAtTimeAndPositionWithAudioclip.Add(clone.GetInstanceID(), new KeyValuePair<float, KeyValuePair<GameObject, AudioClip>>(Time.time, cloneWithAudiclip));
        
    }

    public void RemoveFromDictionary(int id)
    {
        instantiatedPrefabAtTimeAndPositionWithAudioclip.Remove(id);
    }
}
