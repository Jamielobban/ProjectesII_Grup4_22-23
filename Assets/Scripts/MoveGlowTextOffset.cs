using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGlowTextOffset : MonoBehaviour
{
    Material circleMat;    
    float scrollSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        circleMat = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed;        
        circleMat.SetTextureOffset("_GlowTex", new Vector2(offset, 0));
        Debug.Log(circleMat.GetTextureOffset("_GlowTex"));
    }
}
