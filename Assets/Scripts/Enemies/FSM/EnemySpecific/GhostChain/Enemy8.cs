using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy8 : Entity
{
    public E8_ChasingState chasingState { get; private set; }
    public E8_DeadState deadState { get; private set; }    
    public E8_SlashState slashState { get; private set; }
    

    [SerializeField]
    private D_ChaseState chasingStateData;    
    [SerializeField]
    private D_DeadState deadStateData;    
    [SerializeField]
    private D_SlashState slashStateData;

    public bool inRange;

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

        chasingState = new E8_ChasingState(this, stateMachine, "chase", chasingStateData, this);
        slashState = new E8_SlashState(this, stateMachine, "slash", slashStateData, this);
        deadState = new E8_DeadState(this, stateMachine, "dead", deadStateData, this);        

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
