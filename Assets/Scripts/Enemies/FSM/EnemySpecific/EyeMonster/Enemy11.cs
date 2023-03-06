using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy11 : Entity
{
    public E11_FiringState firingState { get; private set; }
    public E11_DeadState deadState { get; private set; }
    public E11_IdleState idleState { get; private set; }

    [SerializeField]
    private D_FiringState firingStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;

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
        
        firingState = new E11_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E11_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E11_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(idleState);

        agent.speed = enemyData.speed;
    }

    public override void Update()
    {
        base.Update();

        if (isDead && stateMachine.currentState != deadState)
        {
            stateMachine.ChangeState(deadState);
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
