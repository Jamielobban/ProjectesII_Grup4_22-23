using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy10 : Entity
{

    public E10_ChasingState chasingState { get; private set; }
    public E10_DeadState deadState { get; private set; }
    public E10_IdleState idleState { get; private set; }
    public E10_FiringState firingState { get; private set; }

    [SerializeField]
    private D_ChaseState chasingStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_FiringState firingStateData;

    public float distanceToPassToIdle;

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

        chasingState = new E10_ChasingState(this, stateMachine, "chase", chasingStateData, this);
        firingState = new E10_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E10_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E10_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(chasingState);        

        agent.speed = enemyData.speed;
    }

    public override void Update()
    {
        base.Update();

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
        }

        if (!isDead && stateMachine.currentState != idleState && vectorToPlayer.magnitude >= distanceToPassToIdle && vectorToPlayer.magnitude >= enemyData.stopDistanceFromPlayer)
        {
            stateMachine.ChangeState(idleState);
        }
    }

    protected override void GetDamage(float damageHit)
    {
        base.GetDamage(damageHit);
    }

    public void SearchFunction(string funcName)
    {
        if (this.gameObject == null || stateMachine.currentState == null || stateMachine.currentState == deadState)
            return;
        var exampleType = stateMachine.currentState.GetType();
        var exampleMethod = exampleType.GetMethod(funcName);
        exampleMethod.Invoke(stateMachine.currentState, null);

        if (this.gameObject == null)
        {
            CancelInvoke();
        }
    }
}
