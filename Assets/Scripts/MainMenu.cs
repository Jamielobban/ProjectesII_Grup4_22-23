using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    public AudioClip menuTheme;
    void Start()
    {
        PlayerPrefs.DeleteAll();
        AudioManager.Instance.LoadSound(menuTheme, GameObject.FindGameObjectWithTag("MainCamera").transform, 0, true, false, 0.7f);
        Time.timeScale = 1;
    }

    
}
