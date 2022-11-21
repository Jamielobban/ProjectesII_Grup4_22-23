using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour
{
    enum ChallengeGameState { PICKCHALLENGE, SHOWCHALLENGEINSCREEN, WAITINGCHOICE, WAITINGRESULT, WAITINGTIMENEWCHALLENGE };

    [SerializeField]
    List<ChallengeController> allGameChallenges = new List<ChallengeController>();
    ChallengeController currentChallenge;
    ChallengeGameState stateController;
    bool challengeAccepted;
    float lastTimeChallengeExit;
    [SerializeField]
    float timeBetweenChallenges;
    float timeGameStart;
    ChallengeValue valueDependingOnTimePlayed;

    ChallengeController actualChallenge;
    int actualChallengeIndex;
    public ChallengeManager()
    {
        AddToList(new KillsLow(), new KillsMediumLow(), new KillsMedium(), new KillsMediumHigh(), new KillsMediumHigh());
    }

    private void Start()
    {
        stateController = ChallengeGameState.WAITINGTIMENEWCHALLENGE;
        timeBetweenChallenges = Random.Range(5, 8);
        challengeAccepted = false;
        lastTimeChallengeExit = Time.time;
        timeGameStart = Time.time;

        DontDestroyOnLoad(this);

    }

    private void Update()
    {

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
                actualChallenge = PickChallengeFromPool();
                actualChallenge.Start();
                stateController = ChallengeGameState.SHOWCHALLENGEINSCREEN;
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

    ChallengeController PickChallengeFromPool()
    {
        ChallengeController challengeToReturn;
        do {actualChallengeIndex = Random.Range(0, allGameChallenges.Count); challengeToReturn = allGameChallenges[actualChallengeIndex]; } while (challengeToReturn.challenge.value != valueDependingOnTimePlayed);
        Debug.Log(challengeToReturn.challenge.value);
        return challengeToReturn;
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
            allGameChallenges.RemoveAt(actualChallengeIndex);
            timeBetweenChallenges = Random.Range(5, 8);
            stateController = ChallengeGameState.WAITINGTIMENEWCHALLENGE;
            lastTimeChallengeExit = Time.time;
        }
    }

    void ShowChallengeInScreen()
    {
        stateController = ChallengeGameState.WAITINGCHOICE;
        QuestionDialogUI.Instance.ShowQuestion(actualChallenge.challenge.challengeText, () => {
            stateController = ChallengeGameState.WAITINGRESULT;
        }, () => {
            lastTimeChallengeExit = Time.time;
            stateController = ChallengeGameState.WAITINGTIMENEWCHALLENGE;
        });
    }    

    void ApplyChallengePerk()
    {
        //TODO:
        Debug.Log("Achived");
    }

    void ApplyChallengePunishment()
    {
        //TODO:
        Debug.Log("Lose");

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


