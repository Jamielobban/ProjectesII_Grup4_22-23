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
        var blockParticles = GameObject.Instantiate(stateData.blockParticles, enemy.transform.position + enemy.shieldPos, Quaternion.identity);
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
        stateMachine.ChangeState(enemy.chasingState);
    }
}
