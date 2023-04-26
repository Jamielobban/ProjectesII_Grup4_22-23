using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundAtStart : MonoBehaviour
{
    [SerializeField]
    AudioClip[] soundsToPlay;
    [SerializeField]
    bool toLoop = true;

    public List<int?> soundsToPlayKeys = new List<int?>();
    // Start is called before the first frame update
    void Start()
    {
       for(int i = 0; i < soundsToPlay.Length; i++)
       {
            var k = AudioManager.Instance.LoadSound(soundsToPlay[i], this.transform, 0, toLoop,true, MixerGroups.SFX,1);
            if (k.HasValue)
            {
                soundsToPlayKeys.Add(k);
            }
            else
            {
                soundsToPlayKeys.Add(null);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
