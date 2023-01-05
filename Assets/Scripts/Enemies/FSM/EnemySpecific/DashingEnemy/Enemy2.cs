using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    //public bool isDashing;
    //public bool canDash;
    public E2_ChasingState chasingState { get; private set; }
    public E2_IdleState idleState { get; private set; }
    public E2_DashingState dashingState { get; private set; }
    public E2_DeadState deadState { get; private set; }


    [SerializeField]
    private D_ChaseState chasingStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_DashState dashStateData;
    [SerializeField]
    private D_DeadState deadStateData;


    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override Transform GetBurnValues()
    {
        throw new System.NotImplementedException();
    }

    public override void GetDamage(float damageHit, HealthStateTypes damageType, float knockBackForce, Vector3 bulletPosition, TransformMovementType type)
    {
        base.GetDamage(damageHit, damageType, knockBackForce, bulletPosition, type);
    }

    public override void Start()
    {
        base.Start();

        chasingState = new E2_ChasingState(this, stateMachine, "chase", chasingStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        dashingState = new E2_DashingState(this, stateMachine, "dash", dashStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);

        //isDashing = false;
        //canDash = true;

        stateMachine.Initialize(idleState);
    }

    public override void Update()
    {
        base.Update();

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }
    }    

    
}
