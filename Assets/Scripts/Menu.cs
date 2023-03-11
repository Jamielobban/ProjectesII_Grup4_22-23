using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {

        if(!(PlayerPrefs.GetInt("SalaPrincipal") != 0))
        {
            PlayerPrefs.SetInt("isDead", (true ? 1 : 0));

            SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 3));

        }
        else
        {
            SceneManager.LoadScene(2);

        }

    }
}
