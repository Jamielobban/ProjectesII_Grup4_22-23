using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy9 : Entity
{
    public E9_ChasingState chasingState { get; private set; }
    public E9_DeadState deadState { get; private set; }
    public E9_IdleState idleState { get; private set; }    
    public E9_FiringState firingState { get; private set; }

    [SerializeField]
    private D_ChaseState chasingStateData;
    [SerializeField]
    private D_DeadState deadStateData;
    [SerializeField]
    private D_IdleState idleStateData;   
    [SerializeField]
    private D_FiringState firingStateData;

    public bool enemyVariant;
    public float distanceToPassToIdle;
    public float lastTimeFliped;

    //variant 0 jump llarg

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

        if (enemyVariant)
        {
            anim.SetInteger("variant", 1);
        }
        else
        {
            anim.SetInteger("variant", 0);
        }

        chasingState = new E9_ChasingState(this, stateMachine, "chase", chasingStateData, this);
        firingState = new E9_FiringState(this, stateMachine, "fire", firingStateData, this);
        deadState = new E9_DeadState(this, stateMachine, "dead", deadStateData, this);
        idleState = new E9_IdleState(this, stateMachine, "idle", idleStateData, this);

        stateMachine.Initialize(chasingState);
        lastTimeFliped = 0;

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

        if (stateMachine.currentState != firingState && stateMachine.currentState != idleState && stateMachine.currentState != deadState && agent.enabled /*&& Time.time - lastTimeFliped >= 0.6f*/)
        {
            if (agent.desiredVelocity.x < 0.2f)
            {
                Vector3 aux = this.transform.localScale;
                aux.x = -Mathf.Abs(aux.x);
                this.transform.localScale = aux;
                lastTimeFliped = Time.time;
            }
            else if (agent.desiredVelocity.x > -0.2f)
            {
                Vector3 aux = this.transform.localScale;
                aux.x = Mathf.Abs(aux.x);
                this.transform.localScale = aux;
                lastTimeFliped = Time.time;
            }
            else
            {
                if (stateMachine.currentState == chasingState)
                {
                    if (player.transform.position.x > this.transform.position.x)
                    {
                        Vector3 aux = this.transform.localScale;
                        aux.x = -Mathf.Abs(aux.x);
                        this.transform.localScale = aux;
                        lastTimeFliped = Time.time;

                    }
                    else if (player.transform.position.x < this.transform.position.x)
                    {
                        Vector3 aux = this.transform.localScale;
                        aux.x = Mathf.Abs(aux.x);
                        this.transform.localScale = aux;
                        lastTimeFliped = Time.time;

                    }
                }
            }

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
