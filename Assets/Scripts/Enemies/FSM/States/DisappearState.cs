using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearState : State
{
    protected D_DisappearState stateData;
    public DisappearState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DisappearState stateData) : base(entity, stateMachine, animBoolName)
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
