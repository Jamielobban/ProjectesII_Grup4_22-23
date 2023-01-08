using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_IdleState : IdleState
{
    private Enemy2 enemy;
    private bool firstTimeInIdle = true;
    int? idleSoundKey;

    public E2_IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        idleSoundKey = AudioManager.Instance.LoadSound(stateData.idleSounds, enemy.transform, 0, true);

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
        if (idleSoundKey.HasValue)
        {
            AudioManager.Instance.RemoveAudio(idleSoundKey.Value);
        }
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
