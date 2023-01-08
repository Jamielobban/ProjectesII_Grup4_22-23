using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_ChasingState : ChasingState
{
    private Enemy2 enemy;
    private Vector3 myCheck;
    int? spinSoundKey;

    public E2_ChasingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        spinSoundKey = AudioManager.Instance.LoadSound(stateData.followSounds, enemy.transform, 0, true);
    }

    public override void Exit()
    {
        base.Exit();
        if (spinSoundKey.HasValue)
        {
            AudioManager.Instance.RemoveAudio(spinSoundKey.Value);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        inRange = entity.vectorToPlayer.magnitude < enemy.enemyData.stopDistanceFromPlayer;

        if (inRange)
        {
            stateMachine.ChangeState(enemy.dashingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (((angle < 90 && angle > -90) && enemy.transform.localScale.x < 0) || ((angle > 90 || angle < -90) && enemy.transform.localScale.x > 0))
        {
            enemy.transform.localScale = new Vector3(enemy.transform.localScale.x * -1, enemy.transform.localScale.y, enemy.transform.localScale.z);

        }

        myCheck = Vector3.MoveTowards(enemy.transform.position, enemy.player.transform.position, 0.1f);
        enemy.transform.position = myCheck;
    }
}
