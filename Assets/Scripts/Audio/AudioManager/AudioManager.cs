using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject audioSourcePrefab;
    [SerializeField] FloatValue musicTime;
    public static AudioManager Instance { get; private set; }
    const float maxDifferenceToBePlayed = 0.01f;
    
    Dictionary<string, float> audioNameAndItsRange = new Dictionary<string, float>();
    public class AudioInfo : MonoBehaviour
    {
        public string audioClipName;        
        public float startSoundTime;
        public GameObject audioSorcePrefabClone;

        private AudioSource audioSource;
        private float thisTime;
        private float timeDelayed;
        private bool clipHasStarted = false;

        public AudioInfo(AudioClip clip, float creationTime, Transform parent, GameObject audioSourcePrefab, float maxDistance = 25,float delay = 0f, bool loop = false, bool isSFX = true, float volume = 1)
        {
            thisTime = Time.time;
            timeDelayed = delay;

            audioSorcePrefabClone = Instantiate(audioSourcePrefab, parent);

            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().defaultSoundValue = volume;
            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().amIsfx = isSFX;
            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().myId = audioSorcePrefabClone.GetInstanceID();



            audioSource = audioSorcePrefabClone.GetComponent<AudioSource>();

            audioSource.maxDistance = maxDistance;

            audioSource.loop = loop;

            //if (isSFX)
            //{
            //    if (audioSorcePrefabClone.GetComponent<AudioPrefabScript>().sfxEnabled.RuntimeValue)
            //    {
            //        audioSource.volume = volume * audioSorcePrefabClone.GetComponent<AudioPrefabScript>().sfxValue.RuntimeValue;
            //    }
            //    else
            //    {
            //        audioSource.volume = 0;
            //    }
            //}
            //else
            //{
            //    if (audioSorcePrefabClone.GetComponent<AudioPrefabScript>().musciEnabled.RuntimeValue)
            //    {
            //        audioSource.volume = volume * audioSorcePrefabClone.GetComponent<AudioPrefabScript>().musciValue.RuntimeValue;
            //    }
            //    else
            //    {
            //        audioSource.volume = 0;
            //    }
            //}

            audioSource.clip = clip;
            audioClipName = clip.name;

            startSoundTime = creationTime + delay;

            audioSource.PlayDelayed(delay);
        }

        public AudioInfo(AudioClip clip, float creationTime, Vector3 position, GameObject audioSourcePrefab, float maxDistance = 25, float delay = 0f, bool loop = false, bool isSFX = true, float volume = 1)
        {
            thisTime = Time.time;
            timeDelayed = delay;

            audioSorcePrefabClone = Instantiate(audioSourcePrefab, position, Quaternion.identity);

            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().defaultSoundValue = volume;
            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().amIsfx = isSFX;
            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().myId = audioSorcePrefabClone.GetInstanceID();

            audioSource = audioSorcePrefabClone.GetComponent<AudioSource>();

            audioSource.maxDistance = maxDistance;

            audioSource.loop = loop;

            //if (isSFX)
            //{
            //    if (audioSorcePrefabClone.GetComponent<AudioPrefabScript>().sfxEnabled.RuntimeValue)
            //    {
            //        audioSource.volume = volume * audioSorcePrefabClone.GetComponent<AudioPrefabScript>().sfxValue.RuntimeValue;
            //    }
            //    else
            //    {
            //        audioSource.volume = 0;
            //    }
            //}
            //else
            //{
            //    if (audioSorcePrefabClone.GetComponent<AudioPrefabScript>().musciEnabled.RuntimeValue)
            //    {
            //        audioSource.volume = volume * audioSorcePrefabClone.GetComponent<AudioPrefabScript>().musciValue.RuntimeValue;
            //    }
            //    else
            //    {
            //        audioSource.volume = 0;
            //    }
            //}

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

        audioNameAndItsRange.Add("EnemyBulletWorm", 15);
        audioNameAndItsRange.Add("EnemyBulletWizard", 15);
        audioNameAndItsRange.Add("idle", 31);
        audioNameAndItsRange.Add("Candle Fire Flicker Sound Effect (mp3cut.net)", 3.5f);
        audioNameAndItsRange.Add("TorchFire", 8f);
        audioNameAndItsRange.Add("GotaSound", 7f);
        audioNameAndItsRange.Add("ArrowImpact", 2.5f);
        audioNameAndItsRange.Add("elevator", 1000f);
        audioNameAndItsRange.Add("slashVerd",15);
        
    }

    private void Update()
    {
        RemoveEmptyPositions();

        int num = 0;
        foreach (var aux in audiosPlaying)
        {
            if (!aux.Value.audioSorcePrefabClone.GetComponent<AudioSource>().isPlaying)
            {
                num++;
            }
        }
        //Debug.Log(num);

        //Debug.Log(audiosPlaying.Count);
        foreach (var pair in audiosPlaying)
        {
            if(pair.Value.audioClipName == "DoorOpening") { 
            
                Debug.Log(pair.Value.audioClipName);
            }
            
        }
    }

    public int? LoadSound(AudioClip clip, Vector3 position, float delay = 0f, bool loop = false, bool isSFX = true, float volume = 1)
    {
        if(!CheckIfShouldPlay(clip, delay))
        {
            return null;
        }

        float range;

        if (audioNameAndItsRange.ContainsKey(clip.name))
        {
            range = audioNameAndItsRange[clip.name];
        }
        else
        {
            range = 25;
        }

        AudioInfo audioInfo = new AudioInfo(clip, Time.time, position, audioSourcePrefab, range, delay, loop, isSFX, volume);

        audioInfo.audioSorcePrefabClone.GetComponent<AudioPrefabScript>().myId = audioInfo.audioSorcePrefabClone.GetInstanceID();
        audiosPlaying.Add(audioInfo.audioSorcePrefabClone.GetInstanceID(), audioInfo);

        return audioInfo.audioSorcePrefabClone.GetInstanceID();
    }

    public int? LoadSound(AudioClip clip, Transform parent, float delay = 0f, bool loop = false, bool isSFX = true, float volume = 1)
    {
        if (!CheckIfShouldPlay(clip, delay))
        {
            //Debug.Log("in");
            return null;

        }

        float range;

        if (audioNameAndItsRange.ContainsKey(clip.name))
        {
            range = audioNameAndItsRange[clip.name];
        }
        else
        {
            range = 25;
        }

        AudioInfo audioInfo = new AudioInfo(clip, Time.time, parent, audioSourcePrefab, range, delay, loop, isSFX, volume);

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

    public AudioSource GetAudioFromDictionaryIfPossible(int key)
    {
        if (audiosPlaying.ContainsKey(key) && audiosPlaying[key].audioSorcePrefabClone != null)
        {
            return audiosPlaying[key].audioSorcePrefabClone.GetComponent<AudioSource>();
        }
        return null;
    }

    public float GetMusicTime()
    {
        return musicTime.RuntimeValue;
    }

    public void SetMusicTime(float time)
    {
        musicTime.RuntimeValue = time;
    }

    public void ChangeDefaultVolumeValueOfAudio(int key, float newDefaultValue)
    {
        Transform prefab;
        var aux = GetAudioFromDictionaryIfPossible(key);

        if(aux != null)
        {
            prefab = aux.transform;
            prefab.GetComponent<AudioPrefabScript>().defaultSoundValue = newDefaultValue;

        }
       



        //if (prefab != null)
        //{
        //}
    }

    private bool CheckIfShouldPlay(AudioClip clip, float delay)
    {
        if(clip.name == "Attack2" || clip.name == "TurretAttackBo" || clip.name == "ArrowImpact" || clip.name == "skeletonShield" || clip.name == "Hitmarker")
        {
            return audiosPlaying.Where(aI => aI.Value.audioSorcePrefabClone != null && aI.Value.audioSorcePrefabClone.GetComponent<AudioSource>().clip.name == clip.name && (Mathf.Abs(aI.Value.startSoundTime - (Time.time + delay)) <= maxDifferenceToBePlayed)).ToList().Count == 0;
        }
        return true;
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
