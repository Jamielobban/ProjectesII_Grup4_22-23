using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsMedium : EnemyKillsInTime
{
    public KillsMedium()
    {
        challenge.value = ChallengeValue.MEDIUM;

        System.Random r = new System.Random();

        int rand = r.Next(0, 2);
        int number;

        if (rand == 0)
        {
            number = r.Next(7, 12);
            numberEnemiesToKill = number;
            number = r.Next(14, 18);
            challenge.durationChallengeTime = number; //2          
        }
        else
        {
            number = r.Next(26, 30);
            numberEnemiesToKill = number;
            number = r.Next(52, 60);
            challenge.durationChallengeTime = number; //2              
        }

        base.SetTextTimeAndNumberenemies();
    }
    //public override void Start()
    //{
    //    base.Start();

        
    //}

    
}
