using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    public Texture2D[] arrayTextures;
    SpriteRenderer sr;
    Material mat;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTexture(int arrayTexturePosition)
    {
        sr.material.SetTexture("_GlowTex", arrayTextures[arrayTexturePosition]);
        //Debug.Log(arrayTexturePosition);
    }
}
