using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E12_IdleState : IdleState
{
    Enemy12 enemy;
    float waitTime;
    float enterTime;
    public E12_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy12 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        waitTime = Random.Range(1.5f, 5f);
        enterTime = Time.time;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(Time.time - enterTime >= waitTime)
        {
            stateMachine.ChangeState(enemy.travelState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
