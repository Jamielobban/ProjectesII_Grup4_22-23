using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public
    Sprite[] allSprites;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = allSprites[Random.Range(0, allSprites.Length)];
    }
}
