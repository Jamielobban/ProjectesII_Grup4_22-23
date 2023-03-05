using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E9_SlashState : SlashState
{
    Enemy9 enemy;
    public E9_SlashState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_SlashState stateData, Enemy9 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
