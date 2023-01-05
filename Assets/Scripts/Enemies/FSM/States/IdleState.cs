using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    // Start is called before the first frame update
    protected D_IdleState stateData;
    protected bool inRange;
    protected float angle;

    public IdleState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_IdleState stateData) : base(entity, stateMachine, animBoolName)
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

        angle = Mathf.Atan2(entity.vectorToPlayer.normalized.y, entity.vectorToPlayer.normalized.x) * Mathf.Rad2Deg;

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        //entity.rb.rotation = angle;

    }


}


