using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Normal Menu")]
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
    GameObject InfiniteModeButton;

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


    [SerializeField]
    GameObject NewGameMenu;

    [SerializeField]
    GameObject PlayedBeforeMenu;
    //[SerializeField] Animator doorAnimation;



    [SerializeField]
    GameObject NewButton2;
    [SerializeField]
    GameObject LoadButton2;
    [SerializeField]
    GameObject CreditsButton2;
    [SerializeField]
    GameObject SettingsButton2;
    [SerializeField]
    GameObject ExitButton2;
    [SerializeField]
    GameObject BackFromCredits2;
    [SerializeField]
    GameObject BackFromSettings2;
    [SerializeField]
    GameObject Credits2;
    [SerializeField]
    GameObject Settings2;
    [SerializeField]
    GameObject Infinity2;

    // Start is called before the first frame update
    void Start()
    {

        

        if(PlayerPrefs.GetInt("FirstTime", 0) == 0)
        {
            NewGameMenu.SetActive(true);
            PlayerPrefs.DeleteAll();

            for(int i = 0; i < 70; i++)
            {
                string nameSave = "Sala" + i;
                PlayerPrefs.SetInt(nameSave, (false ? 1 : 0));

                if(i == 1|| i == 2)
                    PlayerPrefs.SetInt(nameSave, (true ? 1 : 0));

            }
        }
        else
        {
            if(PlayedBeforeMenu != null)
            {
                PlayedBeforeMenu.SetActive(true);
            }
            else
            {
                return;
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
     

        
        //SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 3));
    }

    public void Continue()
    {
        NewButton2.GetComponentInChildren<Button>().interactable = false;
        LoadButton2.GetComponentInChildren<Button>().interactable = false;
        CreditsButton2.GetComponentInChildren<Button>().interactable = false;
        SettingsButton2.GetComponentInChildren<Button>().interactable = false;
        Infinity2.GetComponentInChildren<Button>().interactable = false;
        ExitButton2.GetComponentInChildren<Button>().interactable = false;
        GameName.SetActive(false);


        StartCoroutine(WaitForLoad());
    }

    public void OpenDoorAnimation()
    {
        //Debug.Log("Hellooooo");
        //doorAnimation.SetBool("hasPressedPlay",true);

    }

    public void InfiniteMode()
    {
        PlayButton.GetComponentInChildren<Button>().interactable = false;
        SettingsButton.GetComponentInChildren<Button>().interactable = false;
        CreditsButton.GetComponentInChildren<Button>().interactable = false;
        ExitButton.GetComponentInChildren<Button>().interactable = false;
        GameName.SetActive(false);
        InfiniteModeButton.GetComponentInChildren<Button>().interactable = false;
        StartCoroutine(WaitForInfiniteMode());
    }

    public void InfiniteMode2()
    {
        LoadButton2.GetComponentInChildren<Button>().interactable = false;
        NewButton2.GetComponentInChildren<Button>().interactable = false;
        SettingsButton2.GetComponentInChildren<Button>().interactable = false;
        CreditsButton2.GetComponentInChildren<Button>().interactable = false;
        ExitButton2.GetComponentInChildren<Button>().interactable = false;
        Infinity2.GetComponentInChildren<Button>().interactable = false;
        GameName.SetActive(false);
        StartCoroutine(WaitForInfiniteMode());
    }

    public void StartNew()
    {
        PlayButton.GetComponentInChildren<Button>().interactable = false;
        SettingsButton.GetComponentInChildren<Button>().interactable = false;
        CreditsButton.GetComponentInChildren<Button>().interactable = false;
        InfiniteModeButton.GetComponentInChildren<Button>().interactable = false;
        ExitButton.GetComponentInChildren<Button>().interactable = false;
        GameName.SetActive(false);
        StartCoroutine(WaitForLoadNewGame());
        
        //PlayerPrefs.DeleteAll();        
        // SceneManager.LoadScene(1);
    }

    public void StartNewGame()
    {
        NewButton2.GetComponentInChildren<Button>().interactable = false;
        LoadButton2.GetComponentInChildren<Button>().interactable = false;
        CreditsButton2.GetComponentInChildren<Button>().interactable = false;
        SettingsButton2.GetComponentInChildren<Button>().interactable = false;
        Infinity2.GetComponentInChildren<Button>().interactable = false;
        ExitButton2.GetComponentInChildren<Button>().interactable = false;
        GameName.SetActive(false);

        StartCoroutine(WaitForLoadNewGame());

        //PlayerPrefs.DeleteAll();        
        // SceneManager.LoadScene(1);
    }

    public void CreditsFromLoadGame()
    {
        LoadButton2.SetActive(false);
        NewButton2.SetActive(false);
        CreditsButton2.SetActive(false);
        SettingsButton2.SetActive(false);
        InfiniteModeButton.SetActive(false);
        GameName.SetActive(false);
        ExitButton2.SetActive(false);

        CreditThings.SetActive(true);
        BackButtonCredits.SetActive(true);
    }

    public void SeetingsFromLoadGame()
    {
        LoadButton2.SetActive(false);
        NewButton2.SetActive(false);
        GameName.SetActive(false);
        InfiniteModeButton.SetActive(false);
        CreditsButton2.SetActive(false);
        SettingsButton2.SetActive(false);
        ExitButton2.SetActive(false);

        SettingsThings.SetActive(true);
        BackButtonCredits.SetActive(true);
    }
    public void BackToMainStart()
    {
        PlayButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
        Debug.Log("When dooes this happen");

        PlayThings.SetActive(false);
      
    }


    public void BackButtonCreditsLoad()
    {

    }

    public void ExitGame()
    {
        Debug.Log("Exit");

        Application.Quit();

    }
    private IEnumerator WaitForLoadNewGame()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Load now");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("FirstTime", 2);
        SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 1));
    }

    private IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Load now");
        PlayerPrefs.SetInt("isDead", 1);
        PlayerPrefs.SetInt("FirstTime", 2);
        SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 1));
    }

    private IEnumerator WaitForInfiniteMode()
    {
        yield return new WaitForSeconds(3.0f);
        Debug.Log("Load now");
        SceneManager.LoadScene(12);
    }
    public void Credits()
    {
        PlayButton.SetActive(false);
        CreditsButton.SetActive(false);
        ExitButton.SetActive(false);
        GameName.SetActive(false);
        InfiniteModeButton.SetActive(false);

        SettingsButton.SetActive(false);
       
        BackButtonCredits.SetActive(true);
        CreditThings.SetActive(true);        
    }

    public void Credits2Load()
    {
        NewButton2.SetActive(false);
        LoadButton2.SetActive(false);
        CreditsButton2.SetActive(false);
        ExitButton2.SetActive(false);
        GameName.SetActive(false);
        Infinity2.SetActive(false);

        SettingsButton2.SetActive(false);

        BackFromCredits2.SetActive(true);
        Credits2.SetActive(true);
    }

    public void Settings2Load()
    {
        NewButton2.SetActive(false);
        LoadButton2.SetActive(false);
        CreditsButton2.SetActive(false);
        ExitButton2.SetActive(false);
        GameName.SetActive(false);
        Infinity2.SetActive(false);

        SettingsButton2.SetActive(false);

        BackFromSettings2.SetActive(true);
        Settings2.SetActive(true);
    }

    public void BackToStartingMenuFromCredits()
    {
        //PlayButton.SetActive(true);
        //CreditsButton.SetActive(true);
        //ExitButton.SetActive(true);
        //SettingsButton.SetActive(true);

        //BackButtonCredits.SetActive(false);
        StartCoroutine(WaitForCreditsToMenu());
        StartCoroutine(SetObjectInactive(CreditThings));
        //CreditThings.SetActive(false);
    }

    public void BackToStartingMenuFromCredits2()
    {
        //PlayButton.SetActive(true);
        //CreditsButton.SetActive(true);
        //ExitButton.SetActive(true);
        //SettingsButton.SetActive(true);

        //BackButtonCredits.SetActive(false);
        StartCoroutine(WaitForCreditsToMenu2());
        StartCoroutine(SetObjectInactive(Credits2));
        //CreditThings.SetActive(false);
    }

    private IEnumerator WaitForCreditsToMenu()
    {
        yield return new WaitForSeconds(0.4f);
        PlayButton.SetActive(true);
        CreditsButton.SetActive(true);
        GameName.SetActive(true);
        ExitButton.SetActive(true);
        SettingsButton.SetActive(true);
        InfiniteModeButton.SetActive(true);

        BackButtonCredits.SetActive(false);
    }

    private IEnumerator WaitForCreditsToMenu2()
    {
        yield return new WaitForSeconds(0.4f);
        NewButton2.SetActive(true);
        LoadButton2.SetActive(true);
        CreditsButton2.SetActive(true);
        ExitButton2.SetActive(true);
        SettingsButton2.SetActive(true);
        GameName.SetActive(true);
        Infinity2.SetActive(true);

        BackFromCredits2.SetActive(false);
    }
    private IEnumerator SetObjectInactive(GameObject gameObject)
    {
        yield return new WaitForSeconds(0.6f);
        gameObject.SetActive(false);
    }

    public void BackToStartingMenuFromSettings()
    {
        PlayButton.SetActive(true);
        CreditsButton.SetActive(true);
        ExitButton.SetActive(true);
        GameName.SetActive(true);
        SettingsButton.SetActive(true);
        InfiniteModeButton.SetActive(true);

        BackButtonSettings.SetActive(false);
        SettingsThings.SetActive(false);
    }

    public void BackToStartingMenuFromSettings2()
    {
        NewButton2.SetActive(true);
        LoadButton2.SetActive(true);
        CreditsButton2.SetActive(true);
        ExitButton2.SetActive(true);
        SettingsButton2.SetActive(true);
        GameName.SetActive(true);    
        Infinity2.SetActive(true);

        BackFromSettings2.SetActive(false);
        Settings2.SetActive(false);
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
        GameName.SetActive(false);
        BackMainButton.SetActive(false);

        OpenSettingsButton.SetActive(false);
        InfiniteModeButton.SetActive(false);

        GamePausedText.SetActive(false);
        SettingsThings.SetActive(true);
        BackToPauseButton.SetActive(true);
    }

    public void SettingsFromIngame()
    {
        ResumeButton.SetActive(false);

        BackMainButton.SetActive(false);

        OpenSettingsButton.SetActive(false);
        //InfiniteModeButton.SetActive(false);

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
        InfiniteModeButton.SetActive(false);
        GameName.SetActive(false);
        ExitButton.SetActive(false);
        SettingsButton.SetActive(false);
        BackButtonSettings.SetActive(true);
        SettingsThings.SetActive(true);
    }
}
