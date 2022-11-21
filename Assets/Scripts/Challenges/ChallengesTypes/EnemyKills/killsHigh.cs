using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillsHigh : EnemyKillsInTime
{
    public KillsHigh()
    {
        challenge.value = ChallengeValue.HIGH;

        System.Random r = new System.Random();

        int rand = r.Next(0, 2);
        int number;

        if (rand == 0)
        {
            number = r.Next(30, 40);
            numberEnemiesToKill = number;
            number = r.Next(50, 60);
            challenge.durationChallengeTime = number; //1.67       
        }
        else
        {
            number = r.Next(8, 10);
            numberEnemiesToKill = number;
            number = r.Next(12, 15);
            challenge.durationChallengeTime = number; //1.5              
        }

        base.SetTextTimeAndNumberenemies();
    }
    //public override void Start()
    //{
    //    base.Start();
        
    //}
        

    
}
