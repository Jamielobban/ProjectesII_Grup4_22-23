using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_BlockState : BlockState
{
    private Enemy4 enemy;
    public E4_BlockState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_BlockState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }
    public override void Enter()
    {
        base.Enter();
        //blockSoundKey = AudioManager.Instance.LoadSound(stateData.blockSound, enemy.transform);
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

    public void BackToChase()
    {
        Debug.Log("in");
        stateMachine.ChangeState(enemy.chasingState);
    }
}
