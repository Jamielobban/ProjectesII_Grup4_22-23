using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsMediumLow : EnemyKillsInTime
{
    public KillsMediumLow()
    {
        challenge.value = ChallengeValue.MEDIUMLOW;


        System.Random r = new System.Random();

        int rand = r.Next(0, 2);
        int number;

        if (rand == 0)
        {
            number = r.Next(4, 9);
            numberEnemiesToKill = number;
            number = r.Next(12, 27);
            challenge.durationChallengeTime = number; //3            
        }
        else
        {
            number = r.Next(18, 20);
            numberEnemiesToKill = number;
            number = r.Next(54, 60);
            challenge.durationChallengeTime = number; //3               
        }

        base.SetTextTimeAndNumberenemies();
    }
    //public override void Start()
    //{
    //    base.Start();


        

    //}

    
}
