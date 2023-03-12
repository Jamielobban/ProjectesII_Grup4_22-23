using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject MenuContainer;
    [SerializeField]
    GameObject SettingThings;
    [SerializeField]
    GameObject BackPausedButton;
    [SerializeField]
    GameObject BackPausedClick;
    [SerializeField]
    GameObject ResumeButton;
    [SerializeField]
    GameObject ResumeClick;
    [SerializeField]
    GameObject OpenSettingsButton;
    [SerializeField]
    GameObject OpenSettingsClick;
    [SerializeField]
    GameObject BackMainButton;
    [SerializeField]
    GameObject BackMainClick;
    [SerializeField]
    GameObject GamePausedText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuContainer.activeInHierarchy)
            {
                if (SettingThings.activeInHierarchy)
                {
                    ResumeButton.SetActive(true);
                    ResumeClick.SetActive(true);
                    BackMainButton.SetActive(true);
                    BackMainClick.SetActive(true);
                    OpenSettingsButton.SetActive(true);
                    OpenSettingsClick.SetActive(true);
                    GamePausedText.SetActive(true);
                    SettingThings.SetActive(false);
                    BackPausedButton.SetActive(false);
                    BackPausedClick.SetActive(false);
                }
                else
                {
                    MenuContainer.SetActive(false);
                    Time.timeScale = 1;
                }                
            }
            else
            {
                MenuContainer.SetActive(true);
                SettingThings.SetActive(false);
                BackPausedButton.SetActive(false);
                BackPausedClick.SetActive(false);
                ResumeButton.SetActive(true);
                ResumeClick.SetActive(true);
                BackMainButton.SetActive(true);
                BackMainClick.SetActive(true);
                OpenSettingsButton.SetActive(true);
                OpenSettingsButton.SetActive(true);
                GamePausedText.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }

    public void ClosePauseMenu()
    {
        MenuContainer.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMain()
    {
        MenuContainer.SetActive(false);
        //Time.timeScale = 1;
        SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 0));

    }
}
