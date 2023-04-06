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
    GameObject ResumeButton;

    [SerializeField]
    GameObject OpenSettingsButton;

    [SerializeField]
    GameObject BackMainButton;

    [SerializeField]
    GameObject GamePausedText;

    [SerializeField]
    BoolValue gamePaused;
    [SerializeField]
    GameObject playerBlur;

    void Awake()
    {
        playerBlur = GameObject.FindGameObjectWithTag("PlayerBlur");
        playerBlur.transform.localScale = new Vector3(4, 3, 1);
        playerBlur.SetActive(false);
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
                    Debug.Log("Now paused in if");
                    ResumeButton.SetActive(true);
                    BackMainButton.SetActive(true);
                    OpenSettingsButton.SetActive(true);
                    GamePausedText.SetActive(true);
                    SettingThings.SetActive(false);
                    BackPausedButton.SetActive(false);
                }
                else
                {
                    MenuContainer.SetActive(false);
                    playerBlur.SetActive(false);
                    Debug.Log("Now unpaused");
                    Time.timeScale = 1;
                }                
            }
            else
            {
                Debug.Log("Now paused in else");
                MenuContainer.SetActive(true);
                SettingThings.SetActive(false);
                BackPausedButton.SetActive(false);
                ResumeButton.SetActive(true);
                BackMainButton.SetActive(true);
                OpenSettingsButton.SetActive(true);
                OpenSettingsButton.SetActive(true);
                GamePausedText.SetActive(true);
                playerBlur.SetActive(true);
                Time.timeScale = 0;
            }
        }

        if (MenuContainer.activeInHierarchy)
        {
            gamePaused.RuntimeValue = true;
        }
        else
        {
            gamePaused.RuntimeValue = false;
        }
    }

    public void ClosePauseMenu()
    {
        MenuContainer.SetActive(false);
        playerBlur.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToMain()
    {
        MenuContainer.SetActive(false);
        //Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
}
