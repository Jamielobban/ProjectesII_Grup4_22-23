using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject PlayButton;
    [SerializeField]
    GameObject CreditsButton;
    [SerializeField]
    GameObject ExitButton;
    [SerializeField]
    GameObject BackButton;
    [SerializeField]
    GameObject PlayClick;
    [SerializeField]
    GameObject CreditsClick;
    [SerializeField]
    GameObject ExitClick;
    [SerializeField]
    GameObject BackClick;
    [SerializeField]
    GameObject GameName;
    [SerializeField]
    GameObject CreditThings;
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
            SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 1));

        }
        else
        {
            SceneManager.LoadScene(2);

        }

    }

    public void ExitGame()
    {
        Debug.Log("Exit");

        Application.Quit();

    }

    public void Credits()
    {
        PlayButton.SetActive(false);
        CreditsButton.SetActive(false);
        ExitButton.SetActive(false);
        PlayClick.SetActive(false);
        CreditsClick.SetActive(false);
        ExitClick.SetActive(false);
        BackButton.SetActive(true);
        BackClick.SetActive(true);
        CreditThings.SetActive(true);        
    }

    public void BackToStartingMenu()
    {
        PlayButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
        PlayClick.SetActive(true);
        CreditsClick.SetActive(true);
        ExitClick.SetActive(true);
        BackButton.SetActive(false);
        BackClick.SetActive(false);
        CreditThings.SetActive(false);
    }
}
