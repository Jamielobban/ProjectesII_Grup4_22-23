using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DashingState : DashState
{
    Enemy2 enemy;    
    Vector3 dashPos;
    int? dashSoundKey;

    public E2_DashingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DashState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        dashPos = enemy.player.transform.position;

        dashSoundKey = AudioManager.Instance.LoadSound(stateData.dashSound, enemy.transform);

        FunctionTimer.Create(() =>
        {
            if (!enemy.GetIfIsDead())
            {
                stateMachine.ChangeState(enemy.idleState);

            }
        }, 0.6f);
    }

    public override void Exit()
    {
        base.Exit();

        if (dashSoundKey.HasValue)
        {
            AudioManager.Instance.RemoveAudio(dashSoundKey.Value);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.transform.position = Vector3.MoveTowards(this.enemy.transform.position, dashPos, 0.45f);

    }
}
