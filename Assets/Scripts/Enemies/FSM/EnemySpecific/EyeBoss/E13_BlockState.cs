using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E13_BlockState : BlockState
{
    Enemy13 enemy;
    public E13_BlockState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_BlockState stateData, Enemy13 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
