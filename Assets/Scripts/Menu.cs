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
    [SerializeField]
    GameObject SettingsThings;
    [SerializeField]
    GameObject BackToPauseButton;
    [SerializeField]
    GameObject BackToPauseClick;
    [SerializeField]
    GameObject GamePausedText;
    [SerializeField]
    GameObject MenuContainer;
    [SerializeField]
    GameObject MenuTopParent;
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
    BoolValue musicEnabled;
    [SerializeField]
    BoolValue sfxEnabled;
    [SerializeField]
    FloatValue musicValue;
    [SerializeField]
    FloatValue sfxValue;




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

    public void MusicEnabledChange(bool newValue)
    {
        musicEnabled.RuntimeValue = newValue;        
    }
    public void SFXEnabledChange(bool newValue)
    {
        sfxEnabled.RuntimeValue = newValue;        
    }
    public void ResumeGame()
    {
        MenuTopParent.GetComponent<PauseMenu>().ClosePauseMenu();
    }

    public void BackToMain()
    {
        MenuTopParent.GetComponent<PauseMenu>().GoToMain();
    }

    public void Settings()
    {
        ResumeButton.SetActive(false);
        ResumeClick.SetActive(false);
        BackMainButton.SetActive(false);
        BackMainClick.SetActive(false);
        OpenSettingsButton.SetActive(false);
        OpenSettingsClick.SetActive(false);
        GamePausedText.SetActive(false);
        SettingsThings.SetActive(true);
        BackToPauseButton.SetActive(true);
        BackToPauseClick.SetActive(true);

    }

    public void BackToPauseMenu()
    {
        ResumeButton.SetActive(true);
        ResumeClick.SetActive(true);
        BackMainButton.SetActive(true);
        BackMainClick.SetActive(true);
        OpenSettingsButton.SetActive(true);
        OpenSettingsClick.SetActive(true);
        GamePausedText.SetActive(true);
        SettingsThings.SetActive(false);
        BackToPauseButton.SetActive(false);
        BackToPauseClick.SetActive(false);
    }

    public void SFXValueChanged(float value)
    {
        sfxValue.RuntimeValue = value;
    }

    public void MusicValueChanged(float value)
    {
        musicValue.RuntimeValue = value;
    }
}
