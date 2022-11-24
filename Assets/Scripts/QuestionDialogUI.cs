using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class QuestionDialogUI : MonoBehaviour
{
    enum BetState { NOSELECTED, WEAPON, STAMINA, TIMEPOWERUP, RANDOMPERK, HEALTH}
    public static QuestionDialogUI Instance { get; private set; }

    [SerializeField]
    GameObject healthBar;
    [SerializeField]
    GameObject powerupTimer;
    [SerializeField]
    GameObject staminaCooldowns;
    

    private TextMeshProUGUI textMeshPro;
    private Button yesBtn;
    private Button noBtn;
    Action _yesAction;
    Action _noAction;
    BetState actualSelection;
    ChallengeValue actualChallengeValue;

    private void Awake()
    {
        Instance = this;

        textMeshPro = transform.Find("Text").GetComponent<TextMeshProUGUI>();
        yesBtn = transform.Find("YesBtn").GetComponent<Button>();
        noBtn = transform.Find("NoBtn").GetComponent<Button>();

        Hide();
        //ShowQuestion("Do yo want to do this?", () => {
        //    Debug.Log("Yes");
        //}, () => {
        //    Debug.Log("No");
        //});
    }

    public void ShowQuestion(string questionText, Action yesAction, Action noAction)
    {
        gameObject.SetActive(true);

        textMeshPro.text = questionText;

        _yesAction = yesAction;
        _noAction = noAction;

        Time.timeScale = 0;

        actualSelection = BetState.NOSELECTED;
        actualChallengeValue = ChallengeValue.UNKNOWN;
        //yesBtn.onClick.AddListener( () =>{
        //    Hide();
        //    yesAction();           
        //});
        //noBtn.onClick.AddListener(() => {
        //    Hide();
        //    noAction();            
        //});
    }

    public void YesBtn()
    {
        if(actualSelection != BetState.NOSELECTED)
        {
            Hide();
            _yesAction();
            Time.timeScale = 1;
            Debug.Log(actualSelection);
        }       

    }
    public void NoBtn()
    {
        Hide();
        _noAction();
        Time.timeScale = 1;
    }

    public void WeaponSelected()
    {
        actualSelection = BetState.WEAPON;
    }
    public void StaminaSelected()
    {
        actualSelection = BetState.STAMINA;
    }
    public void PerkSelected()
    {
        actualSelection = BetState.RANDOMPERK;
    }
    public void TimePowerupSelected()
    {
        actualSelection = BetState.TIMEPOWERUP;
    }
    public void HealthSelected()
    {
        actualSelection = BetState.HEALTH;
    }

    public void ApplySelectionReward()
    {
        switch (actualChallengeValue)
        {
            case ChallengeValue.LOW:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:
                        List<RectTransform> healthObjects = healthBar.GetComponentsInChildren<RectTransform>().ToList<RectTransform>();
                        foreach(RectTransform healthObject in healthObjects)
                        {
                            //float percent = healthObject.rect.width *100 / 100;                            
                            //healthObject.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0); 
                            healthObject.sizeDelta = new Vector2(healthObject.sizeDelta.x + 10, healthObject.sizeDelta.y);

                        }
                        break;
                    default:
                        break;
                }
                
                break;
            case ChallengeValue.MEDIUMLOW:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:

                        break;
                    default:
                        break;
                }

                break;
            case ChallengeValue.MEDIUM:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:

                        break;
                    default:
                        break;
                }

                break;
            case ChallengeValue.MEDIUMHIGH:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:

                        break;
                    default:
                        break;
                }

                break;
            case ChallengeValue.HIGH:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:

                        break;
                    default:
                        break;
                }

                break;           
            default:
                break;
        }
    }
    public void ApplySelectionPunishment()
    {
        switch (actualChallengeValue)
        {
            case ChallengeValue.LOW:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:

                        break;
                    default:
                        break;
                }

                break;
            case ChallengeValue.MEDIUMLOW:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:

                        break;
                    default:
                        break;
                }

                break;
            case ChallengeValue.MEDIUM:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:

                        break;
                    default:
                        break;
                }

                break;
            case ChallengeValue.MEDIUMHIGH:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:

                        break;
                    default:
                        break;
                }

                break;
            case ChallengeValue.HIGH:

                switch (actualSelection)
                {
                    case BetState.WEAPON:
                        //TODO:
                        break;
                    case BetState.STAMINA:

                        break;
                    case BetState.TIMEPOWERUP:

                        break;
                    case BetState.RANDOMPERK:

                        break;
                    case BetState.HEALTH:

                        break;
                    default:
                        break;
                }

                break;
            default:
                break;
        }
    }
     

    public void SetActualChallengeDifficulty(ChallengeValue valueToSet)
    {
        actualChallengeValue = valueToSet;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
