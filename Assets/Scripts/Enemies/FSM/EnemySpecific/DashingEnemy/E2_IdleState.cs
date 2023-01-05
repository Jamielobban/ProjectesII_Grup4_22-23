using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_IdleState : State
{
    private Enemy2 enemy;
    private bool firstTimeInIdle = true;
    public E2_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        if (firstTimeInIdle)
        {
            firstTimeInIdle = false;
            stateMachine.ChangeState(enemy.chasingState);
        }
        else
        {

            FunctionTimer.Create(() =>
            {
                if (!enemy.GetIfIsDead())
                {
                    stateMachine.ChangeState(enemy.chasingState);

                }
            }, 2.5f);
        }

    }    

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
