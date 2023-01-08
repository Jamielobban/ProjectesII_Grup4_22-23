using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsMediumHigh : EnemyKillsInTime
{
    public KillsMediumHigh()
    {
        challenge.value = ChallengeValue.MEDIUMHIGH;

        System.Random r = new System.Random();

        int rand = r.Next(0, 2);
        int number;

        if (rand == 0)
        {
            number = r.Next(28, 32);
            numberEnemiesToKill = number;
            number = r.Next(50, 54);
            challenge.durationChallengeTime = number; //1.8          
        }
        else
        {
            number = r.Next(7, 13);
            numberEnemiesToKill = number;
            number = r.Next(12, 17);
            challenge.durationChallengeTime = number; //1.8              
        }

        base.SetTextTimeAndNumberenemies();
    }
    //public override void Start()
    //{
    //    base.Start();

        
    //}

    
}
