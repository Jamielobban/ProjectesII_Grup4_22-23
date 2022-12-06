using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringState : State
{
    protected D_FiringState stateData;
    protected bool inRange;
    protected float angle;
    protected float lastShootTime;

    public FiringState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_FiringState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        lastShootTime = 0;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        angle = Mathf.Atan2(entity.vectorToPlayer.y, entity.vectorToPlayer.x) * Mathf.Rad2Deg;        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        entity.rb.rotation = angle;
    }
}
