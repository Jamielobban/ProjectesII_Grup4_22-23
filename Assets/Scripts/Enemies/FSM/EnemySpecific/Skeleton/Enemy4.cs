using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Entity
{
    public E4_ChasingState chasingState { get; private set; }
    public E4_SlashState slashState { get; private set; }
    public E4_DeadState deadState { get; private set; }
    public E4_IdleState idleState { get; private set; }
    public E4_BlockState blockState { get; private set; }

    [SerializeField]
    private D_ChaseState chasingStateData;
    [SerializeField]
    private D_SlashState slashStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField] 
    private D_BlockState blockStateData;

    public float distanceToPassToIdle;
    public Vector3 shieldPos;
    public bool attack0attack1 = false;

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override Transform GetBurnValues()
    {
        throw new System.NotImplementedException();
    }    

    public override void Start()
    {
        base.Start();

        //chasingStateData = chaseStateDatasVariants[variant];
        //firingStateData = firingStateDatasVariants[variant];

        chasingState = new E4_ChasingState(this, stateMachine, "chase", chasingStateData, this);
        slashState = new E4_SlashState(this, stateMachine, "slash", slashStateData, this);
        deadState = new E4_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E4_IdleState(this, stateMachine, "idle", idleStateData, this);
        blockState = new E4_BlockState(this, stateMachine, "block", blockStateData, this);

        stateMachine.Initialize(chasingState);
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

    public override void GetDamage(float damageHit, HealthStateTypes damageType, float knockBackForce, Vector3 bulletPosition, TransformMovementType type)
    {
        if (stateMachine.currentState != chasingState)
        {
            base.GetDamage(damageHit, damageType, knockBackForce, bulletPosition, type);
        }
        else
        {
            stateMachine.ChangeState(blockState);
        }
    }
    public void WhichAttackDo()
    {
        attack0attack1 = Random.Range(0, 100) % 2 == 0;
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
