using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalaPrincipal : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("SalaPrincipal", (true ? 1 : 0));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
