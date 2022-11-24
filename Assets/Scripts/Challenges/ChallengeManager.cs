using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ChallengeManager : MonoBehaviour
{
    [SerializeField]
    float timeBetweenChallenges;
    enum ChallengeGameState { PICKCHALLENGE, SHOWCHALLENGEINSCREEN, WAITINGCHOICE, WAITINGRESULT, WAITINGTIMENEWCHALLENGE };

    ChallengeController actualChallenge;

    List<ChallengeController> allGameChallenges = new List<ChallengeController>();

    ChallengeValue valueDependingOnTimePlayed;
    ChallengeGameState stateController;    
    float lastTimeChallengeExit;    
    float timeGameStart;

    

    public ChallengeManager()
    {
        AddToList(new KillsLow(), new KillsMediumLow(), new KillsMedium(), new KillsMediumHigh(), new KillsMediumHigh());
    }

    private void Start()
    {
        stateController = ChallengeGameState.WAITINGTIMENEWCHALLENGE;
        timeBetweenChallenges = Random.Range(5, 8);        
        lastTimeChallengeExit = Time.time;
        timeGameStart = Time.time;        

        DontDestroyOnLoad(this);

    }

    private void Update()
    {
        //Debug.Log(stateController);

        if(Time.time - timeGameStart <= 120) //2 min o menys
        {
            valueDependingOnTimePlayed = ChallengeValue.LOW;
        }
        else if (Time.time - timeGameStart <= 240) //4 min o menys
        {
            valueDependingOnTimePlayed = ChallengeValue.MEDIUMLOW;
        } 
        else if(Time.time - timeGameStart <= 360) //6 min o menys
        {
            valueDependingOnTimePlayed = ChallengeValue.MEDIUM;
        }
        else if(Time.time - timeGameStart <= 480) //8 min o menys
        {
            valueDependingOnTimePlayed = ChallengeValue.MEDIUMHIGH;
        }
        else //8 cap adalt
        {
            valueDependingOnTimePlayed = ChallengeValue.HIGH;
        }

        //Debug.Log(valueDependingOnTimePlayed);


        switch (stateController)
        {
            case ChallengeGameState.WAITINGTIMENEWCHALLENGE:
                WaitingTimeNewChallengeLogicUpdate();
                break;
            case ChallengeGameState.PICKCHALLENGE:
                PickChallengeFromPool();
                break;
            case ChallengeGameState.SHOWCHALLENGEINSCREEN:
                ShowChallengeInScreen();
                break;
            //case ChallengeGameState.WAITINGCHOICE:
            //    WaitingIfChallngeAccepted();
            //    break;            
            case ChallengeGameState.WAITINGRESULT:
                WaitingChallengeResult();
                break;            
            default:
                break;
        }
    }


    void WaitingTimeNewChallengeLogicUpdate()
    {
        if(Time.time - lastTimeChallengeExit >= timeBetweenChallenges)
        {
            stateController = ChallengeGameState.PICKCHALLENGE;
        }
    }

    void PickChallengeFromPool()
    {
        //ChallengeController challengeToReturn;
        List<ChallengeController> checkChallengesAvailable = allGameChallenges.Where(challenge => challenge.challenge.value == valueDependingOnTimePlayed).ToList();
        if(checkChallengesAvailable.Count != 0)
        {            
            actualChallenge = checkChallengesAvailable[Random.Range(0, checkChallengesAvailable.Count)];
            Debug.Log(actualChallenge.challenge.value);
            stateController = ChallengeGameState.SHOWCHALLENGEINSCREEN;
        }
        else
        {
            
        }        
        
    }

    void WaitingChallengeResult()
    {
        if (actualChallenge.Update())
        {
            if (actualChallenge.GetIfAchived())
            {
                ApplyChallengePerk();                
            }
            else
            {
                ApplyChallengePunishment();                
            }
            allGameChallenges.RemoveAt(allGameChallenges.IndexOf(actualChallenge));
            timeBetweenChallenges = Random.Range(3, 4);
            stateController = ChallengeGameState.WAITINGTIMENEWCHALLENGE;
            lastTimeChallengeExit = Time.time;
        }
    }

    void ShowChallengeInScreen()
    {
        stateController = ChallengeGameState.WAITINGCHOICE;
        QuestionDialogUI.Instance.ShowQuestion(actualChallenge.challenge.challengeText, () => {
            actualChallenge.Start();
            stateController = ChallengeGameState.WAITINGRESULT;
        }, () => {
            lastTimeChallengeExit = Time.time;
            stateController = ChallengeGameState.WAITINGTIMENEWCHALLENGE;
        });
        QuestionDialogUI.Instance.SetActualChallengeDifficulty(actualChallenge.challenge.value);
    }    

    void ApplyChallengePerk()
    {
        //TODO:
        Debug.Log("Achived");
        QuestionDialogUI.Instance.GetComponent<QuestionDialogUI>().ApplySelectionReward();
    }

    void ApplyChallengePunishment()
    {
        //TODO:
        Debug.Log("Lose");
        QuestionDialogUI.Instance.GetComponent<QuestionDialogUI>().ApplySelectionPunishment();

    }

    // Function with a variable number of parameters
    void AddToList(params ChallengeController[] list)
    {
        for (int i = 0; i < list.Length; i++)
        {
            allGameChallenges.Add(list[i]);
        }
    }
}


