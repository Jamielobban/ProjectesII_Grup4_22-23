using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6_ChasingState : ChasingState
{
    Enemy6 enemy;
    public E6_ChasingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData, Enemy6 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
