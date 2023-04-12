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
    PlayerMovement player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        forestMusic = AudioManager.Instance.LoadSound(forestAudio, player.transform, 0f, true, false, 1);
    }

}
