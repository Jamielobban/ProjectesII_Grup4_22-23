using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Transition : MonoBehaviour
{

    public SpriteRenderer toClear;

   
    // Start is called before the first frame update
    void Start()
    {

        toClear = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            toClear.DOFade(0, 1f);
        }
    }
}
