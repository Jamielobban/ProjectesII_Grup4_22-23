using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KillsLow : EnemyKillsInTime
{
    public KillsLow()
    {
        challenge.value = ChallengeValue.LOW;

        System.Random r = new System.Random();

        int rand = r.Next(0, 2);
        int number;

        if (rand == 0)
        {
            number = r.Next(3, 7);
            numberEnemiesToKill = number;
            number = r.Next(18, 23);
            challenge.durationChallengeTime = number;
        }
        else 
        {
            number = r.Next(12, 17);
            numberEnemiesToKill = number;
            number = r.Next(55, 60);
            challenge.durationChallengeTime = number;
        }

        base.SetTextTimeAndNumberenemies();
    }
    //public override void Start()
    //{
    //    base.Start();

    //     //6

    //    //if (Random.Range(0, 2) == 0)
    //    //{
    //    //    numberEnemiesToKill = Random.Range(3, 7);
    //    //    challenge.durationChallengeTime = Random.Range(18, 23); //6
    //    //}
    //    //else
    //    //{
    //    //    numberEnemiesToKill = Random.Range(12, 17);
    //    //    challenge.durationChallengeTime = Random.Range(55, 60); //6
    //    //}

    //    base.SetTextTimeAndNumberenemies();
    //}

    
}
