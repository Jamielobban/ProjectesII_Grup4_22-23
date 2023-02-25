using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;
    protected int probabilityOfHearth;
    public DeadState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, D_DeadState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();

        probabilityOfHearth = Random.Range(0, 7);


        if (GameObject.FindGameObjectWithTag("RoomManager") != null)
        {
            GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>().Dead();

        }

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
