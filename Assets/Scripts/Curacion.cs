using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curacion : MonoBehaviour
{
    [SerializeField]
    AudioClip curacionSound;

     void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerMovement>().currentHearts < collision.GetComponent<PlayerMovement>().maxHearts)
                AudioManager.Instance.LoadSound(curacionSound, collision.transform);

            collision.GetComponent<PlayerMovement>().Health();            
            Destroy(this.gameObject);
        }
    }
}
