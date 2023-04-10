using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordForWaveAudio : MonoBehaviour
{    
    AudioSource audioSource;
    public Enemy14 enemy;
    CircleCollider2D circleC;

    private void Start()
    {
        circleC = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        Transform[] transformChilds = this.GetComponentsInChildren<Transform>();

        for(int i = 0; i < transformChilds.Length; i++)
        {
            if(transformChilds[i].TryGetComponent<AudioSource>(out audioSource))
            {
                if(audioSource.clip.name == "fireBlast")
                audioSource.gameObject.transform.position = circleC.bounds.center + ((enemy.player.transform.position - circleC.bounds.center).normalized) * circleC.radius;
            }
        }

        
    }
}
