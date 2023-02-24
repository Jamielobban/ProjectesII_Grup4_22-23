using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : Entity
{
    public E5_ChasingState chasingState { get; private set; }    
    public E5_DeadState deadState { get; private set; }
    public E5_IdleState idleState { get; private set; }
    public E5_FiringState firingState { get; private set; }
    

    [SerializeField]
    private D_ChaseState chasingStateData;
    [SerializeField]
    private D_FiringState firingStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;

    [SerializeField]
    GameObject fireBreathPrefab;
    

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

        chasingState = new E5_ChasingState(this, stateMachine, "chase", chasingStateData, this);
        firingState = new E5_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E5_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E5_IdleState(this, stateMachine, "idle", idleStateData, this);        

        stateMachine.Initialize(chasingState);

        agent.speed = enemyData.speed;
    }

    public override void Update()
    {
        base.Update();
        //Debug.Log(stateMachine.currentState);

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
        try { exampleMethod.Invoke(stateMachine.currentState, null); } catch { /*Debug.Log(funcName); Debug.Log(stateMachine.currentState);*/ };
        if (this.gameObject == null)
        {
            CancelInvoke();
        }
    }
}
