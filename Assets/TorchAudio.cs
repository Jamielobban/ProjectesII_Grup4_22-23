using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAudio : MonoBehaviour
{
    // Start is called before the first frame update
    int? audiokey;
    [SerializeField] AudioClip torch;
    void Start()
    {
        audiokey = AudioManager.Instance.LoadSound(torch, this.transform, 0, true, true, 1);
        audiokey = AudioManager.Instance.LoadSound(torch, this.transform, 0, true, true, 1);
        audiokey = AudioManager.Instance.LoadSound(torch, this.transform, 0, true, true, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
