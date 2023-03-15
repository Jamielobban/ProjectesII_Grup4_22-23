using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingState : State
{
    protected D_ChaseState stateData;
    protected bool inRange;
    protected float angle;

    public ChasingState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_ChaseState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        inRange = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        angle = Mathf.Atan2(entity.vectorToPlayer.normalized.y, entity.vectorToPlayer. normalized.x) * Mathf.Rad2Deg;

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //entity.rb.rotation = angle;

    }


}
