using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestMusic : MonoBehaviour
{
    // Start is called before the first frame update

    int? forestMusic;
    [SerializeField]
    AudioClip forestAudio;

    [SerializeField]
    Camera mainCam;
    void Start()
    {
        mainCam = FindObjectOfType<Camera>();
        forestMusic = AudioManager.Instance.LoadSound(forestAudio, mainCam.transform, 0f, true, false, 1);
    }

}
