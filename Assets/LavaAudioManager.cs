using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip[] lavaAudios;
    [SerializeField] int? audioKey;
    [SerializeField] Transform[] positions;
    [SerializeField] int audioToPlay;
    [SerializeField] int positionToSpawn;
    void Start()
    {
        positions = GetComponentsInChildren<Transform>();
        InvokeRepeating("ChangeAudioAndPlace",0f, 3.8f);
    }

    void ChangeAudioAndPlace()
    {
        Debug.Log("Repeat");
        audioToPlay = Random.Range(0, lavaAudios.Length);
        positionToSpawn = Random.Range(0, positions.Length);
        audioKey = AudioManager.Instance.LoadSound(lavaAudios[audioToPlay], positions[positionToSpawn], 0, false, true, 1);
    }
}
