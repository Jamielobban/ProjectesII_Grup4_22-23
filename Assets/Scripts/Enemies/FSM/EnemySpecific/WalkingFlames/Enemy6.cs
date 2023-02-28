using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : Entity
{
    public E6_ChasingState chasingState { get; private set; }
    public E6_DeadState deadState { get; private set; }
    public E6_IdleState idleState { get; private set; }

    [SerializeField]
    private D_ChaseState chasingStateData;    
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;


    public Enemy6()
    {

    }

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

        chasingState = new E6_ChasingState(this, stateMachine, "chase", chasingStateData, this);        
        deadState = new E6_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E6_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(chasingState);

        agent.speed = enemyData.speed;
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void GetDamage(float damageHit)
    {
        base.GetDamage(damageHit);
    }
}
