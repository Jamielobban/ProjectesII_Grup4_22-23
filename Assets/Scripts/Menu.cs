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
    GameObject BackButtonCredits;
    [SerializeField]
    GameObject BackButtonSettings;
    [SerializeField]
    GameObject GameName;
    [SerializeField]
    GameObject CreditThings;
    [SerializeField]
    GameObject SettingsThings;
    [SerializeField]
    GameObject BackToPauseButton;

    [SerializeField]
    GameObject GamePausedText;
    [SerializeField]
    GameObject MenuContainer;
    [SerializeField]
    GameObject MenuTopParent;
    [SerializeField]
    GameObject ResumeButton;

    [SerializeField]
    GameObject OpenSettingsButton;

    [SerializeField]
    GameObject BackMainButton;

    [SerializeField]
    GameObject PlayThings;
    [SerializeField]
    GameObject SettingsButton;


    [SerializeField]
    BoolValue musicEnabled;
    [SerializeField]
    BoolValue sfxEnabled;
    [SerializeField]
    FloatValue musicValue;
    [SerializeField]
    FloatValue sfxValue;


    [SerializeField]
    GameObject GreenOnMusic;
    [SerializeField]
    GameObject RedOnMusic;
    [SerializeField]
    GameObject GreenOnSFX;
    [SerializeField]
    GameObject RedOnSFX;

    // Start is called before the first frame update
    void Start()
    {

        

        if(PlayerPrefs.GetInt("FirstTime", 0) == 0)
        {
            PlayerPrefs.DeleteAll();

            for(int i = 0; i < 70; i++)
            {
                string nameSave = "Sala" + i;
                PlayerPrefs.SetInt(nameSave, (false ? 1 : 0));

                if(i == 1)
                    PlayerPrefs.SetInt(nameSave, (true ? 1 : 0));

            }
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

        PlayerPrefs.SetInt("isDead", 1);


        PlayButton.SetActive(false);
        CreditsButton.SetActive(false);
        ExitButton.SetActive(false);
     
        PlayThings.SetActive(true);
     

        //PlayerPrefs.SetInt("FirstTime", 2);
        //SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 3));
    }

    public void Continue()
    {
        PlayerPrefs.SetInt("FirstTime", 2);
        SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 1));
    }

    public void StartNew()
    {
        PlayerPrefs.DeleteAll();        
        SceneManager.LoadScene(1);
    }

    public void BackToMainStart()
    {
        PlayButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
        Debug.Log("When dooes this happen");

        PlayThings.SetActive(false);
      
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
      
        SettingsButton.SetActive(false);
       
        BackButtonCredits.SetActive(true);
        CreditThings.SetActive(true);        
    }

    public void BackToStartingMenuFromCredits()
    {
        PlayButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
        SettingsButton.SetActive(true);

        BackButtonCredits.SetActive(false);
        CreditThings.SetActive(false);
    }

    public void BackToStartingMenuFromSettings()
    {
        PlayButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
        SettingsButton.SetActive(true);

        BackButtonSettings.SetActive(false);
        SettingsThings.SetActive(false);
    }

    public void MusicEnabledChangeOn()
    {
        PlayerPrefs.SetInt("MusicEnabled", (1));
        musicEnabled.RuntimeValue = true;        
    }

    public void MuteButtonOn()
    {
        MusicEnabledChangeOff();
        GreenOnMusic.SetActive(true);
        RedOnMusic.SetActive(false);
    }

    public void MuteButtonOff()
    {
        MusicEnabledChangeOn();
        RedOnMusic.SetActive(true);
        GreenOnMusic.SetActive(false);
    }

    public void SFXButtonOn()
    {
        SFXEnabledChangeOff();
        GreenOnSFX.SetActive(true);
        RedOnSFX.SetActive(false);
    }

    public void SFXButtonOff()
    {
        SFXEnabledChangeOn();
        GreenOnSFX.SetActive(false);
        RedOnSFX.SetActive(true);
    }

    public void MusicEnabledChangeOff()
    {
        PlayerPrefs.SetInt("MusicEnabled", (0));
        musicEnabled.RuntimeValue = false;
    }
    public void SFXEnabledChangeOff()
    {
        PlayerPrefs.SetInt("SFXEnabled", (0));

        sfxEnabled.RuntimeValue = false;        
    }

    public void SFXEnabledChangeOn()
    {
        PlayerPrefs.SetInt("SFXEnabled", (1));

        sfxEnabled.RuntimeValue = true;
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

        BackMainButton.SetActive(false);

        OpenSettingsButton.SetActive(false);

        GamePausedText.SetActive(false);
        SettingsThings.SetActive(true);
        BackToPauseButton.SetActive(true);


    }

    public void BackToPauseMenu()
    {
        ResumeButton.SetActive(true);

        BackMainButton.SetActive(true);

        OpenSettingsButton.SetActive(true);

        GamePausedText.SetActive(true);
        SettingsThings.SetActive(false);
        BackToPauseButton.SetActive(false);

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

    public void OpenSettingsFromMainMenu()
    {
        PlayButton.SetActive(false);
        CreditsButton.SetActive(false);
        ExitButton.SetActive(false);
        SettingsButton.SetActive(false);
        BackButtonSettings.SetActive(true);
        SettingsThings.SetActive(true);
    }
}
