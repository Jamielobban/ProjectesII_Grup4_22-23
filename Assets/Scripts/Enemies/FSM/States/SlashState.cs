using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashState : State
{
    protected D_SlashState stateData;
    protected float angle;
    protected float angleFirePoint;
    public SlashState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SlashState stateData) : base(entity, stateMachine, animBoolName)
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
