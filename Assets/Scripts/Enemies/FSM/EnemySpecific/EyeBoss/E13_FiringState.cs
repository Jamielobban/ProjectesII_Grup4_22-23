using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E13_FiringState : FiringState
{
    Enemy13 enemy;

    float lastTimeEnterBigFatman = 0;
    const float timeInBF = 5;
    bool doingBF = false;


    float lastTimeEnterBulletSpread = 0;
    const float timeInSpread = 3.5f;
    bool doingSpread = false;


    float lastTimeEnterLaserSpinNormal = 0;
    const float timeInLaserSpin = 3;
    bool doingLaserSpin = false;


    float lastTimenterQuadruleLaserSpin = 0;
    const float timeInQLS = 4;
    bool doingQLS = false;

    float lastTimeExitState = 0;
   
    public E13_FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData, Enemy13 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        switch(enemy.mode)
        {
            case 1:
                if(!doingBF && !doingLaserSpin && Time.time - lastTimeExitState >= enemy.waitBetweenAttacks)
                {
                    if(enemy.vectorToPlayer.magnitude <= 10)
                    {
                        doingBF = true;
                        enemy.anim.SetBool("bigFatman", true);
                        lastTimeEnterBigFatman = Time.time;
                    }
                    else
                    {
                        doingLaserSpin = true;
                        enemy.anim.SetBool("laserSpin", true);
                        lastTimeEnterLaserSpinNormal = Time.time;
                    }
                }
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }

        if(doingLaserSpin && Time.time - lastTimeEnterLaserSpinNormal >= timeInLaserSpin)
        {
            doingLaserSpin = false;
            enemy.anim.SetBool("laserSpin", false);
            lastTimeExitState = Time.time;
        }
        else if (doingBF && Time.time - lastTimeEnterBigFatman >= timeInBF)
        {
            doingBF = false;
            enemy.anim.SetBool("bigFatman", false);
            lastTimeExitState = Time.time;
        }
        else if (doingSpread && Time.time - lastTimeEnterBulletSpread >= timeInSpread)
        {
            doingSpread = false;
            enemy.anim.SetBool("eyeSpread", false);
            lastTimeExitState = Time.time;
        }
        else if (doingQLS && Time.time - lastTimenterQuadruleLaserSpin >= timeInQLS)
        {
            doingQLS = false;
            enemy.anim.SetBool("QLS", false);
            lastTimeExitState = Time.time;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
}
