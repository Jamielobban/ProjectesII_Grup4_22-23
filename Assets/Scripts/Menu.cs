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
    GameObject PlayThings;
    [SerializeField]
    GameObject BackClick2;
    [SerializeField]
    GameObject BackButton2;


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

        

        if(PlayerPrefs.GetInt("FirstTime", 0) == 0)
        {
            Debug.Log("Borrar");
            PlayerPrefs.DeleteAll();
        }
        
        if(musicValue != null)
        {
            musicValue.RuntimeValue = PlayerPrefs.GetFloat("musicValue", 1);
        }
        if (sfxValue != null)
        {
            sfxValue.RuntimeValue = PlayerPrefs.GetFloat("sfxValue", 1);
        }
        if (musicEnabled != null)
        {
            musicEnabled.RuntimeValue = PlayerPrefs.GetInt("MusicEnabled", 1) == 1 ? true : false;
        }
        if(sfxEnabled != null)
        {
            sfxEnabled.RuntimeValue = PlayerPrefs.GetInt("MusicEnabled", 1) == 1 ? true : false;

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Play()
    {

        //if ((PlayerPrefs.GetInt("SalaPrincipal", 0) != 0))
        //{
        //    PlayerPrefs.SetInt("isDead", (true ? 1 : 0));

        //    SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 3));

        //}
        //else
        //{
        //    SceneManager.LoadScene(2);

        //}
        //}

        PlayButton.SetActive(false);
        CreditsButton.SetActive(false);
        ExitButton.SetActive(false);
        PlayClick.SetActive(false);
        CreditsClick.SetActive(false);
        ExitClick.SetActive(false);
        PlayThings.SetActive(true);
        BackButton2.SetActive(true);
        BackClick2.SetActive(true);

        //PlayerPrefs.SetInt("FirstTime", 2);
        //SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 3));
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("FirstTime", 2);
        SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 3));
    }

    public void StartNew()
    {
        PlayerPrefs.DeleteAll();        
        SceneManager.LoadScene(3);
    }

    public void BackToMainStart()
    {
        PlayButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
        PlayClick.SetActive(true);
        CreditsClick.SetActive(true);
        ExitClick.SetActive(true);

        PlayThings.SetActive(false);
        BackButton2.SetActive(false);
        BackClick2.SetActive(false);
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
        PlayerPrefs.SetInt("MusicEnabled", (newValue ? 1 : 0));
        musicEnabled.RuntimeValue = newValue;        
    }
    public void SFXEnabledChange(bool newValue)
    {
        PlayerPrefs.SetInt("SFXEnabled", (newValue ? 1 : 0));

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
        PlayerPrefs.SetFloat("sfxValue", value);

        sfxValue.RuntimeValue = value;
    }

    public void MusicValueChanged(float value)
    {
        PlayerPrefs.SetFloat("musicValue", value);

        musicValue.RuntimeValue = value;
    }
}
