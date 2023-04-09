using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OnAudioClick : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btn;
    int? audioClip;
    AudioClip audioThis;
    Camera cam;
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        btn = GetComponent<Button>();
        btn.onClick.AddListener(AudioOnClick);
        audioThis = Resources.Load<AudioClip>("Sounds/ClickAudio");
    }

    public void AudioOnClick()
    {
        audioClip = AudioManager.Instance.LoadSound(audioThis, cam.transform);
    }
}
