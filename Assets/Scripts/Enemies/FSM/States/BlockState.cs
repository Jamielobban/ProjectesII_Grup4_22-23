using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockState : State
{
    protected D_BlockState stateData;
    public BlockState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_BlockState stateData) : base(entity, stateMachine, animBoolName)
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
