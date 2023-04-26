using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Audio;

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

        private AudioMixer mixerMaster;
        private AudioMixer mixerSFX;        
        private AudioMixer mixerImportantSFX;        

        public AudioInfo(AudioClip clip, float creationTime, Transform parent, GameObject audioSourcePrefab, float maxDistance = 25,float delay = 0f, bool loop = false, bool isSFX = true, MixerGroups myGroup = MixerGroups.SFX,float volume = 1)
        {
            mixerMaster = Resources.Load<AudioMixer>("Sounds/ZZMasterMixer");
            mixerSFX = Resources.Load<AudioMixer>("Sounds/ZZSFXMicer");
            mixerImportantSFX = Resources.Load<AudioMixer>("Sounds/ZZImportantSFX");

            thisTime = Time.time;
            timeDelayed = delay;

            audioSorcePrefabClone = Instantiate(audioSourcePrefab, parent);

            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().defaultSoundValue = volume;
            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().amIsfx = isSFX;
            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().myId = audioSorcePrefabClone.GetInstanceID();            

            audioSource = audioSorcePrefabClone.GetComponent<AudioSource>();

            switch (myGroup)
            {
                case MixerGroups.ENEMIES:
                    audioSource.outputAudioMixerGroup = mixerSFX.FindMatchingGroups("Enemies")[0];
                    break;
                case MixerGroups.GUNSHOT:
                    audioSource.outputAudioMixerGroup = mixerImportantSFX.FindMatchingGroups("GunShot")[0];
                    break;
                case MixerGroups.MUSIC:
                    audioSource.outputAudioMixerGroup = mixerMaster.FindMatchingGroups("Music")[0];
                    break;
                case MixerGroups.PLAYER:
                    audioSource.outputAudioMixerGroup = mixerSFX.FindMatchingGroups("Player")[0];
                    break;
                case MixerGroups.SFX:
                    audioSource.outputAudioMixerGroup = mixerMaster.FindMatchingGroups("SoundEffects")[0];
                    break;
                case MixerGroups.ENVIRONMENT:
                    audioSource.outputAudioMixerGroup = mixerSFX.FindMatchingGroups("Environment")[0];
                    break;
                case MixerGroups.HITMARKER:
                    audioSource.outputAudioMixerGroup = mixerImportantSFX.FindMatchingGroups("Hitmarker")[0];
                    break;
                case MixerGroups.OTHER:
                    audioSource.outputAudioMixerGroup = mixerImportantSFX.FindMatchingGroups("Other")[0];
                    break;
            }

            audioSource.maxDistance = maxDistance;

            audioSource.loop = loop;            

            audioSource.clip = clip;
            audioClipName = clip.name;

            startSoundTime = creationTime + delay;

            audioSource.PlayDelayed(delay);
        }

        public AudioInfo(AudioClip clip, float creationTime, Vector3 position, GameObject audioSourcePrefab, float maxDistance = 25, float delay = 0f, bool loop = false, bool isSFX = true, MixerGroups myGroup = MixerGroups.SFX, float volume = 1)
        {
            mixerMaster = Resources.Load<AudioMixer>("Sounds/ZZMasterMixer");
            mixerSFX = Resources.Load<AudioMixer>("Sounds/ZZSFXMicer");
            mixerImportantSFX = Resources.Load<AudioMixer>("Sounds/ZZImportantSFX");

            thisTime = Time.time;
            timeDelayed = delay;

            audioSorcePrefabClone = Instantiate(audioSourcePrefab, position, Quaternion.identity);

            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().defaultSoundValue = volume;
            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().amIsfx = isSFX;
            audioSorcePrefabClone.GetComponent<AudioPrefabScript>().myId = audioSorcePrefabClone.GetInstanceID();           

            audioSource = audioSorcePrefabClone.GetComponent<AudioSource>();

            switch (myGroup)
            {
                case MixerGroups.ENEMIES:
                    audioSource.outputAudioMixerGroup = mixerSFX.FindMatchingGroups("Enemies")[0];
                    break;
                case MixerGroups.GUNSHOT:
                    audioSource.outputAudioMixerGroup = mixerImportantSFX.FindMatchingGroups("GunShot")[0];
                    break;
                case MixerGroups.MUSIC:
                    audioSource.outputAudioMixerGroup = mixerMaster.FindMatchingGroups("Music")[0];
                    break;
                case MixerGroups.PLAYER:
                    audioSource.outputAudioMixerGroup = mixerSFX.FindMatchingGroups("Player")[0];
                    break;
                case MixerGroups.SFX:
                    audioSource.outputAudioMixerGroup = mixerMaster.FindMatchingGroups("SoundEffects")[0];
                    break;
                case MixerGroups.ENVIRONMENT:
                    audioSource.outputAudioMixerGroup = mixerSFX.FindMatchingGroups("Environment")[0];
                    break;
                case MixerGroups.HITMARKER:
                    audioSource.outputAudioMixerGroup = mixerImportantSFX.FindMatchingGroups("Hitmarker")[0];
                    break;
                case MixerGroups.OTHER:
                    audioSource.outputAudioMixerGroup = mixerImportantSFX.FindMatchingGroups("Other")[0];
                    break;
            }

            audioSource.maxDistance = maxDistance;

            audioSource.loop = loop;            

            audioSource.clip = clip;
            audioClipName = clip.name;

            startSoundTime = creationTime + delay;

            audioSource.PlayDelayed(delay);            

        }
        
    }    

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
        audioNameAndItsRange.Add("Candle Fire Flicker Sound Effect (mp3cut.net)", 10f);
        audioNameAndItsRange.Add("TorchFire", 15f);
        audioNameAndItsRange.Add("GotaSound", 28f);
        audioNameAndItsRange.Add("ArrowImpact", 2.5f);
        audioNameAndItsRange.Add("elevator", 1000f);
        audioNameAndItsRange.Add("slashVerd",15);
        //Eye boss
        audioNameAndItsRange.Add("EyeBossLaserCharge", 50f);
        audioNameAndItsRange.Add("EyeBossLaserHum", 20f);
        audioNameAndItsRange.Add("EyeBossBigFatMan", 50f);
        audioNameAndItsRange.Add("EyeBossSquelching", 50f);
        audioNameAndItsRange.Add("lavaNormal", 15);
        audioNameAndItsRange.Add("lavaPitchAgut", 15);
        audioNameAndItsRange.Add("lavaPitchGreu", 15);
        audioNameAndItsRange.Add("chain2", 15);
        audioNameAndItsRange.Add("chain3", 15);
        audioNameAndItsRange.Add("chain4", 15);
        audioNameAndItsRange.Add("chain5", 15);
        audioNameAndItsRange.Add("dashSkull", 10);
        audioNameAndItsRange.Add("skullDead", 13);
        audioNameAndItsRange.Add("SpinSkull", 10);

        
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
            
                //Debug.Log(pair.Value.audioClipName);
            }
            
        }
    }

    public int? LoadSound(AudioClip clip, Vector3 position, float delay = 0f, bool loop = false, bool isSFX = true, MixerGroups myGroup = MixerGroups.SFX, float volume = 1)
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

        AudioInfo audioInfo = new AudioInfo(clip, Time.time, position, audioSourcePrefab, range, delay, loop, isSFX, myGroup, volume);

        audioInfo.audioSorcePrefabClone.GetComponent<AudioPrefabScript>().myId = audioInfo.audioSorcePrefabClone.GetInstanceID();
        audiosPlaying.Add(audioInfo.audioSorcePrefabClone.GetInstanceID(), audioInfo);

        return audioInfo.audioSorcePrefabClone.GetInstanceID();
    }

    public int? LoadSound(AudioClip clip, Transform parent, float delay = 0f, bool loop = false, bool isSFX = true, MixerGroups myGroup = MixerGroups.SFX, float volume = 1)
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

        AudioInfo audioInfo = new AudioInfo(clip, Time.time, parent, audioSourcePrefab, range, delay, loop, isSFX, myGroup, volume);

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
