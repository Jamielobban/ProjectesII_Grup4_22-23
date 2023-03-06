using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E11_IdleState : IdleState
{
    Enemy11 enemy;
    float timeLastFlip;
    int flipsDone;
    Vector3 aux;
    float timeBetweenFlips;
    float timeToNextFlips;
    public E11_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy11 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        aux = enemy.transform.localScale;
        aux.x *= -1;
        enemy.transform.localScale = aux;

        timeLastFlip = Time.time;
        flipsDone = 1;

        timeBetweenFlips = Random.Range(1f, 3f);
        timeToNextFlips = Random.Range(3.5f, 7f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Mathf.Abs(enemy.vectorToPlayer.magnitude) < enemy.distanceToPassToIdle)
        {
            stateMachine.ChangeState(enemy.firingState);
        }

        if (flipsDone == 1)
        {
            if (Time.time - timeLastFlip >= timeBetweenFlips)
            {
                aux = enemy.transform.localScale;
                aux.x *= -1;
                enemy.transform.localScale = aux;

                timeLastFlip = Time.time;
                flipsDone = 0;

                timeBetweenFlips = Random.Range(1f, 3f);
            }
        }
        else if (flipsDone == 0)
        {
            if (Time.time - timeLastFlip >= timeToNextFlips)
            {
                aux = enemy.transform.localScale;
                aux.x *= -1;
                enemy.transform.localScale = aux;

                timeLastFlip = Time.time;
                flipsDone = 1;

                timeToNextFlips = Random.Range(3.5f, 7f);

            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
