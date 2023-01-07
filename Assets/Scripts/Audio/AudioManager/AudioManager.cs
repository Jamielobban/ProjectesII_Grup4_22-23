using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject audioSourcePrefab;
    public static AudioManager Instance { get; private set; }
    const float maxDifferenceToBePlayed = 0.01f;

    public class AudioInfo : MonoBehaviour
    {
        public string audioClipName;        
        public float startSoundTime;
        public GameObject audioSorcePrefabClone;

        private AudioSource audioSource;
        private float thisTime;
        private float timeDelayed;
        private bool clipHasStarted = false;

        public AudioInfo(AudioClip clip, float creationTime, Transform parent, GameObject audioSourcePrefab, float delay = 0f, bool loop = false)
        {
            thisTime = Time.time;
            timeDelayed = delay;

            audioSorcePrefabClone = Instantiate(audioSourcePrefab, parent);

            audioSource = audioSorcePrefabClone.GetComponent<AudioSource>();

            audioSource.loop = loop;

            audioSource.clip = clip;
            audioClipName = clip.name;

            startSoundTime = creationTime + delay;

            audioSource.PlayDelayed(delay);
        }

        public AudioInfo(AudioClip clip, float creationTime, Vector3 position, GameObject audioSourcePrefab, float delay = 0f, bool loop = false)
        {
            thisTime = Time.time;
            timeDelayed = delay;

            audioSorcePrefabClone = Instantiate(audioSourcePrefab, position, Quaternion.identity);

            audioSource = audioSorcePrefabClone.GetComponent<AudioSource>();

            audioSource.loop = loop;

            audioSource.clip = clip;
            audioClipName = clip.name;

            startSoundTime = creationTime + delay;

            audioSource.PlayDelayed(delay);            

        }
        
    }

    //[SerializeField] AudioSource sfxAudioSource, musicAudioSource, sfxLoop;


    Dictionary<int, AudioInfo> audiosPlaying = new Dictionary<int, AudioInfo>();  
    

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
        RemoveEmptyPositions();
        Debug.Log(audiosPlaying.Count);
        foreach(var pair in audiosPlaying)
        {
            Debug.Log(pair.Value.audioClipName);
        }
    }

    public int? LoadSound(AudioClip clip, Vector3 position, float delay = 0f, bool loop = false)
    {
        if(!CheckIfShouldPlay(clip, delay))
        {
            return null;
        }

        AudioInfo audioInfo = new AudioInfo(clip, Time.time, position, audioSourcePrefab, delay, loop);

        audioInfo.audioSorcePrefabClone.GetComponent<AudioPrefabScript>().myId = audioInfo.audioSorcePrefabClone.GetInstanceID();
        audiosPlaying.Add(audioInfo.audioSorcePrefabClone.GetInstanceID(), audioInfo);

        return audioInfo.audioSorcePrefabClone.GetInstanceID();
    }

    public int? LoadSound(AudioClip clip, Transform parent, float delay = 0f, bool loop = false)
    {
        if (!CheckIfShouldPlay(clip, delay))
        {
            Debug.Log("in");
            return null;

        }

        AudioInfo audioInfo = new AudioInfo(clip, Time.time, parent, audioSourcePrefab, delay, loop);

        audioInfo.audioSorcePrefabClone.GetComponent<AudioPrefabScript>().myId = audioInfo.audioSorcePrefabClone.GetInstanceID();
        audiosPlaying.Add(audioInfo.audioSorcePrefabClone.GetInstanceID(), audioInfo);

        return audioInfo.audioSorcePrefabClone.GetInstanceID();
    }

    public void RemoveAudio(int id)
    {
        if (audiosPlaying.ContainsKey(id))
        {
            if (audiosPlaying[id].audioSorcePrefabClone != null)
            {
                audiosPlaying[id].audioSorcePrefabClone.GetComponent<AudioSource>().Pause();
            }
            Destroy(audiosPlaying[id].audioSorcePrefabClone);
            audiosPlaying.Remove(id);
        }
        
    }

    private bool CheckIfShouldPlay(AudioClip clip, float delay)
    {
        return audiosPlaying.Where(aI => aI.Value.audioSorcePrefabClone != null && aI.Value.audioSorcePrefabClone.GetComponent<AudioSource>().clip.name == clip.name && (Mathf.Abs(aI.Value.startSoundTime - (Time.time + delay)) <= maxDifferenceToBePlayed)).ToList().Count == 0;        
    }

    private void RemoveEmptyPositions()
    {
        List<int> emptyPositions = new List<int>();
        foreach (KeyValuePair<int, AudioInfo> pair in audiosPlaying)
        {
            if (pair.Value.audioSorcePrefabClone == null)
            {
                emptyPositions.Add(pair.Key);
            }
        }
        foreach (int id in emptyPositions)
        {
            if (audiosPlaying.ContainsKey(id))
            {
                Destroy(audiosPlaying[id].audioSorcePrefabClone);
                audiosPlaying.Remove(id);
            }
        }
        emptyPositions.Clear();
    }

    //IEnumerator WaitToAddInToDictionary(AudioInfo audioInfo, float delay)
    //{
    //    yield return new WaitForSeconds(delay);



    //}

    //public void PlaySound(AudioClip clip, Vector3 pos)
    //{
    //    GameObject clone = Instantiate(audioSourcePrefab, pos, Quaternion.identity);
    //    clone.GetComponent<AudioSource>().PlayOneShot(clip);

    //    KeyValuePair<GameObject,AudioClip> cloneWithAudiclip = new KeyValuePair<GameObject, AudioClip>(clone, clip);
    //    instantiatedPrefabAtTimeAndPositionWithAudioclip.Add(clone.GetInstanceID(), new KeyValuePair<float, KeyValuePair<GameObject, AudioClip>> (Time.time, cloneWithAudiclip));

    //}

    //public void PlaySound(AudioClip clip, Transform parentToSetPrefab)
    //{
    //    GameObject clone = Instantiate(audioSourcePrefab,parentToSetPrefab);
    //    clone.GetComponent<AudioSource>().PlayOneShot(clip);

    //    KeyValuePair<GameObject, AudioClip> cloneWithAudiclip = new KeyValuePair<GameObject, AudioClip>(clone, clip);
    //    instantiatedPrefabAtTimeAndPositionWithAudioclip.Add(clone.GetInstanceID(), new KeyValuePair<float, KeyValuePair<GameObject, AudioClip>>(Time.time, cloneWithAudiclip));

    //}

    //public void PlayLoop(AudioClip clip, Transform parentToSetPrefab)
    //{
    //    GameObject clone = Instantiate(audioSourcePrefab, parentToSetPrefab);
    //    AudioSource audioS = clone.GetComponent<AudioSource>();
    //    audioS.clip = clip;
    //    audioS.loop = true;
    //    audioS.Play();
    //}

    //public void PlaySoundDelayed(AudioClip clip, float delay, Vector3 pos)
    //{
    //    GameObject clone = Instantiate(audioSourcePrefab, pos, Quaternion.identity);
    //    clone.GetComponent<AudioSource>().clip = clip;

    //    clone.GetComponent<AudioSource>().PlayDelayed(delay);

    //    WaitToAddInToDictionary(clip, delay, clone);

    //}

    //public void PlaySoundDelayed(AudioClip clip, float delay, Transform parentToSetPrefab)
    //{
    //    GameObject clone = Instantiate(audioSourcePrefab, parentToSetPrefab);
    //    clone.GetComponent<AudioSource>().clip = clip;

    //    clone.GetComponent<AudioSource>().PlayDelayed(delay);

    //    WaitToAddInToDictionary(clip, delay, clone);

    //}


    ////public AudioSource PlaySoundLoop(AudioClip clip)
    ////{
    ////    sfxLoop.PlayOneShot(clip);
    ////    return sfxLoop;
    ////}

    ////private void ToggleMusic()
    ////{
    ////    musicAudioSource.mute = !musicAudioSource.mute;
    ////}



    //public void RemoveFromDictionary(int id)
    //{
    //    instantiatedPrefabAtTimeAndPositionWithAudioclip.Remove(id);
    //}
}
