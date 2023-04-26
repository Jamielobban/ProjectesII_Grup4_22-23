using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

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

    public AudioMixerSnapshot unpaused;
    public AudioMixerSnapshot paused;

    void Awake()
    {
        playerBlur = GameObject.FindGameObjectWithTag("PlayerBlur");
        playerBlur.transform.localScale = new Vector3(4, 3, 1);
        playerBlur.SetActive(false);
        unpaused = Resources.Load<AudioMixer>("Sounds/ZZMasterMixer").FindSnapshot("Unpaused");
        paused = Resources.Load<AudioMixer>("Sounds/ZZMasterMixer").FindSnapshot("Paused");
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
                    //Debug.Log("Now paused in if");
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
                    //Debug.Log("Now unpaused");
                    Time.timeScale = 1;
                    unpaused.TransitionTo(0);
                }                
            }
            else
            {
                //Debug.Log("Now paused in else");
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
                paused.TransitionTo(0);
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
        unpaused.TransitionTo(0f);
    }

    public void GoToMain()
    {
        MenuContainer.SetActive(false);
        //Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
}
