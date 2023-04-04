using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OnButtonHover : MonoBehaviour
{
    Material mat;
    static Sequence shineSequence;
    float startTime;
    bool done;
    // Start is called before the first frame update
    int? hoverAudioKey;
    AudioClip hoverAudio;
    Camera cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        hoverAudio = Resources.Load<AudioClip>("Sounds/Menu/HoverSound");
    }

    public void OnMouseOver()
    {
        Debug.Log("Mouse over");
        hoverAudioKey = AudioManager.Instance.LoadSound(hoverAudio, cam.transform);
        mat = GetComponent<Image>().material;
        shineSequence = DOTween.Sequence();
        startTime = Time.time;
        done = false;
        shineSequence.Join(mat.DOFloat(1, "_ShineLocation", 0.5f)).SetDelay(Random.Range(0.05f, 0.1f)).Append(mat.DOFloat(0, "_ShineLocation", 0.5f)).SetDelay(Random.Range(0.1f, 0.25f))/*.SetLoops(-1)*/;
    }

    public void OnMouseExit()
    {
        shineSequence.Kill();
    }
}

