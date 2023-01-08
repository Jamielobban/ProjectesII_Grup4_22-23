using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    protected D_DashState stateData;

    public DashState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DashState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
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
