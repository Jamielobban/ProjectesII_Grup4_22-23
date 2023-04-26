using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ForestMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioMixer mixerMaster;
    int? forestMusic;
    [SerializeField]
    AudioClip forestAudio;

    [SerializeField]
    Camera mainCam;
    void Start()
    {
        mixerMaster = Resources.Load<AudioMixer>("Sounds/ZZMasterMixer");
        mainCam = FindObjectOfType<Camera>();
        forestMusic = AudioManager.Instance.LoadSound(forestAudio, mainCam.transform, 0f, true, false, MixerGroups.MUSIC,1);
        if (forestMusic.HasValue)
            AudioManager.Instance.GetAudioFromDictionaryIfPossible(forestMusic.Value).outputAudioMixerGroup = mixerMaster.FindMatchingGroups("Music")[0];
    }

}
